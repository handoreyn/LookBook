using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Bakery.EventBusRabbitMQ;

public class DefaultRabbitMQPersistenConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DefaultRabbitMQPersistenConnection> _logger;
    private readonly int _retryCount;
    private IConnection _connection;
    private bool _disposed;

    private object sync_root = new ();

    public DefaultRabbitMQPersistenConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistenConnection> logger, int retryCount)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _retryCount = retryCount;
    }

    public bool IsConnected => _connection is { IsOpen: true } && !_disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        
        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        try
        {
            _connection.Dispose();
        }
        catch (IOException e)
        {
            _logger.LogCritical(e.ToString());
        }
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect.");;

        lock (sync_root)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        _logger.LogWarning(ex,
                            "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})",
                            $"{time.TotalSeconds:N1}", ex.Message);
                    });

            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });

            if (!IsConnected)
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened.");
                return false;
            }
            
            _connection.ConnectionShutdown+= ConnectionOnConnectionShutdown;
            _connection.CallbackException+=ConnectionOnCallbackException;
            _connection.ConnectionBlocked+=ConnectionOnConnectionBlocked;
       
            return true; 
        }
    }

    private void ConnectionOnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;
        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");
        TryConnect();
    }

    private void ConnectionOnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if(_disposed) return;
        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");
        TryConnect();
    }

    private void ConnectionOnConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        if (_disposed) return;
        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");
        TryConnect();
    }
}