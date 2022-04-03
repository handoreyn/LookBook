using Bakery.SharedKernel.Entities;
using Bakery.SharedKernel.Enums;

namespace Bakery.Member.Core.Entities;

public class StatusActivity : StatusActivityBaseEntity<StatusEnumType>
{
    public StatusActivity(StatusEnumType status) : base(status)
    {
    }
}