using Bakery.SharedKernel.Entities;
using Bakery.SharedKernel.Enums;

namespace Bakery.Member.Core.Entities;

public class SubscriptionStatusActivity : StatusActivityBaseEntity<SubscriptionStatusType>
{
    public SubscriptionStatusActivity(SubscriptionStatusType status) : base(status)
    {
    }
}