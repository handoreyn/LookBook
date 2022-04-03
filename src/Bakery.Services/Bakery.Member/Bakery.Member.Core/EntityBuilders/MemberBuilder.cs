using Bakery.Member.Core.Entities;
using Bakery.SharedKernel.Enums;

namespace Bakery.Member.Core.EntityBuilders;

public class MemberBuilder
{
    private readonly MemberEntity _member;

    public MemberBuilder()
    {
        _member = new MemberEntity();
    }

    public MemberBuilder(MemberEntity member)
    {
        _member = member;
    }

    public MemberBuilder SetUsername(string username)
    {
        _member.Username = username;
        return this;
    }

    public MemberBuilder SetPassword(string password)
    {
        _member.Password = password;
        return this;
    }

    public MemberBuilder SetStatus(StatusEnumType status)
    {
        _member.Status = status;
        _member.StatusActivity.Add(new StatusActivity(status));
        return this;
    }

    public MemberBuilder SetSubscriptionStatus(SubscriptionStatusType status)
    {
        _member.SubscriptionStatus = status;
        _member.SubscriptionStatusActivity.Add(new SubscriptionStatusActivity(status));
        return this;
    }

    public MemberBuilder SetProfilePictureUrl(string profilePictureUrl)
    {
        _member.ProfilePicture = profilePictureUrl;
        return this;
    }

    public MemberBuilder SetBirthDate(DateTime birthDate)
    {
        _member.BirthDate = birthDate;
        return this;
    }

    public MemberBuilder SetContactInformation(ContactEnumType contactType, string address)
    {
        if (_member.ContactInformation.Any(l => l.ContactType == contactType && l.Address.Equals(address))) return this;

        _member.ContactInformation.Add(new Contact(contactType, address));

        return this;
    }

    public MemberBuilder SetCountry(string country)
    {
        _member.Country = country;
        return this;
    }
    public MemberBuilder SetGender(GenderType gender)
    {
        _member.Gender = gender;
        return this;
    }
}