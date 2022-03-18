namespace Bakery.Member.Core.Exceptions;

public class MemberNotFoundException : Exception
{
    public MemberNotFoundException() : base()
    {

    }

    public MemberNotFoundException(string message) : base(message)
    {

    }
}