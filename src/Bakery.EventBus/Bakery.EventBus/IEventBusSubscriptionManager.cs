public interface IEventBusSubscriptionManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;
    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler;
    void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;
    void RemoveSubscription<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;
    bool HasSubscriptionForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}