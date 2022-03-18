namespace Bakery.Member.Core.Entities;

public class SubscriptionStatusActivity : StatusActivityBaseEntity<SubscriptionStatusType>
{
    public SubscriptionStatusActivity(SubscriptionStatusType status) : base(status)
    {
    }
}