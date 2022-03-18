namespace Bakery.Member.Core.Entities;

public class StatusActivity : StatusActivityBaseEntity<StatusEnumType>
{
    public StatusActivity(StatusEnumType status) : base(status)
    {
    }
}