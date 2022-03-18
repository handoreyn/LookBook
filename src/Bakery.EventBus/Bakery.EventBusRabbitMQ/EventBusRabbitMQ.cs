using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Bakery.EventBus;
using Bakery.EventBus.Abstractions;
using Bakery.EventBus.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Bakery.EventBusRabbitMQ;

public class EventBusRabbitMQ : IEventBus, IDisposable
{
    private const string BrokerName = "bakery_event_bus";
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly ILogger<EventBusRabbitMQ> _logger;
    private readonly IEventBusSubscriptionManager _subscriptionManager;
    private readonly int _retryCount;
    private readonly IServiceProvider _serviceProvider;

    private IModel _consumerChannel;
    private string _queueName;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger, IEventBusSubscriptionManager subscriptionManager, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _persistentConnection = persistentConnection?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider;
        _subscriptionManager = subscriptionManager ?? new InMemoryEventBusSubscriptionEventManager();
        _retryCount = int.Parse(configuration["EventBusRetryCount"]);
        _queueName = configuration["SubscriptionClientName"];
        _consumerChannel = CreateConsumerChannel();
        _subscriptionManager.OnEventRemoved+=SubscriptionManagerOnOnEventRemoved;
    }

    private void SubscriptionManagerOnOnEventRemoved(object sender, string e)
    {
        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

        using var channel = _persistentConnection.CreateModel();
        channel.QueueUnbind(queue: _queueName, exchange: BrokerName, routingKey: e);

        if (!_subscriptionManager.IsEmpty) return;
        
        _queueName = string.Empty;
        _consumerChannel.Close();
    }

    public void Publish(IntegrationEvent @event)
    {
        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event {EventId} after {Timeout}s ({ExceptionMessage})",
                    @event.Id, $"{time.TotalSeconds:N1}", ex.Message);
            });

        var eventName = @event.GetType().Name;
        
        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})",@event.Id,eventName);
        
        using var channel = _persistentConnection.CreateModel();

        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(exchange: BrokerName, type: "direct");
        var body = JsonSerializer.SerializeToUtf8Bytes(@event,@event.GetType());

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; //persistent\

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(exchange: BrokerName, routingKey: eventName, mandatory: true,
                basicProperties: properties, body: body);
        });

    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subscriptionManager.GetEventKey<T>();
        DoInternalSubscription(eventName);

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, nameof(TH));
        
        _subscriptionManager.AddSubscription<T,TH>();
        StartBasicConsume();
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _subscriptionManager.HasSubscriptionForEvent(eventName);
        if (containsKey) return;

        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

        using var channel = _persistentConnection.CreateModel();
        channel.QueueBind(queue: _queueName, exchange: BrokerName, routingKey: eventName);
    }

    public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, nameof(TH));
        
        DoInternalSubscription(eventName);
        _subscriptionManager.AddDynamicSubscription<TH>(eventName);
        StartBasicConsume();
    }

    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subscriptionManager.GetEventKey<T>();
        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);
        _subscriptionManager.RemoveSubscription<T,TH>();
    }

    public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        => _subscriptionManager.RemoveDynamicSubscription<TH>(eventName);

    public void Dispose()
    {
        _consumerChannel?.Dispose();
        _subscriptionManager.Clear();
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel == null)
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
        consumer.Received+=ConsumerOnReceived;
        _consumerChannel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
    }

    private async Task ConsumerOnReceived(object sender, BasicDeliverEventArgs @event)
    {
        var eventName = @event.RoutingKey;
        var message = Encoding.UTF8.GetString(@event.Body.ToArray());

        try
        {
            if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "--- ERROR Processing message \"{Message}\"", message);
        }

        _consumerChannel.BasicAck(@event.DeliveryTag, false);
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();
        
        _logger.LogTrace("Creating RabbitMQ consumer channel");
        var channel = _persistentConnection.CreateModel();
        channel.ExchangeDeclare(exchange:BrokerName,type: "direct");

        channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.CallbackException += (sender, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel...");
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (!_subscriptionManager.HasSubscriptionForEvent(eventName))
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            return;
        }

        await using var scope = _serviceProvider.CreateAsyncScope();
        
        var subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);

        foreach (var subscription in subscriptions)
        {
            if (subscription.IsDynamic)
            {
                var handler =
                    scope.ServiceProvider.GetService(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                if(handler == null) continue;
                dynamic eventData = JsonDocument.Parse(message);

                await Task.Yield();
                await handler.Handle(eventData);
            }
            else
            {
                var handler = scope.ServiceProvider.GetService(subscription.HandlerType);
                if(handler ==null) continue;

                var eventType = _subscriptionManager.GetEventTypeByName(eventName);
                var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                await Task.Yield();
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new[] { integrationEvent });
            }
        }
    }
}