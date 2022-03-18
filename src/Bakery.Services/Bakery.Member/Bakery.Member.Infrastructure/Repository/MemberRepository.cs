using Bakery.Member.Core.Dtos.Member;
using Bakery.Member.Core.Entities;
using Bakery.Member.Core.Exceptions;
using Bakery.Member.Core.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace Bakery.Member.Infrastructure.Repository;

public class MemberRepository : RepositoryBase<MemberEntity>, IMemberRepository
{
    public MemberRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<MemberProfileDto> GetMemberProfileDto(string memberId)
    {
        var query = Filter.Eq(m => m.Id, ObjectId.Parse(memberId));
        var member = await FindAsync(query);
        if (member == null) throw new MemberNotFoundException(
            $"Member not found for specified member id: {memberId}");

        var profile = new MemberProfileDto
        {
            MemberId = member.Id.ToString(),
            Email = member.ContactInformation.FirstOrDefault(l => l.IsPrimary
                                                                  && l.ContactType == ContactEnumType.email)?.Address,
            CreateDate = member.CreateDate,
            SubscriptionStatus = member.SubscriptionStatus,
            Username = member.Username,
            Country = member.Country,
            ProfilePictureUrl = member.ProfilePicture
        };

        return profile;
    }

    public async Task<MemberProfileDto> FindMember(MemberSignInDto model)
    {
        var query = Filter.And(
            Filter.Eq(m => m.Username, model.Username),
            Filter.Eq(m => m.Password, model.Password));

        var member = await FindAsync(query);
        if (member == null) throw new MemberNotFoundException("Member not found by specified username & password");

        return new MemberProfileDto
        {
            MemberId = member.Id.ToString(),
            Username = member.Username,
            CreateDate = member.CreateDate,
            SubscriptionStatus = member.SubscriptionStatus,
            ProfilePictureUrl = member.ProfilePicture,
            Email = member.ContactInformation.FirstOrDefault(l => l.IsPrimary && l.ContactType == ContactEnumType.email)
                ?.Address,
            Country = member.Country
        };
    }

    public async Task<MemberProfileDto> FindMember(string username)
    {
        var query = Filter.Eq(l => l.Username, username);
        var member = await FindAsync(query);
        
        if (member == null) throw new MemberNotFoundException("Member not found by specified username.");
        
        return new MemberProfileDto
        {
            MemberId = member.Id.ToString(),
            Username = member.Username,
            CreateDate = member.CreateDate,
            SubscriptionStatus = member.SubscriptionStatus,
            ProfilePictureUrl = member.ProfilePicture,
            Email = member.ContactInformation.FirstOrDefault(l => l.IsPrimary && l.ContactType == ContactEnumType.email)
                ?.Address,
            Country = member.Country
        };
    }
    
    public async Task<bool> IsMemberExist(string username)
    {
        var query = Filter.Eq(m => m.Username, username);
        var result = await Collection.CountDocumentsAsync(query);
        return result > 0;
    }

    public async Task<MemberRegisterCreateResultDto> Register(MemberRegisterCreateDto model)
    {
        var member = new MemberEntity
        {
            Username = model.Username,
            Password = model.Password,
            ContactInformation = new List<Contact>
            {
                new Contact(ContactEnumType.email,model.Email,true)
            },
            Status = StatusEnumType.active,
            StatusActivity = new()
            {
                new StatusActivity(StatusEnumType.active)
            }
        };


        await Collection.InsertOneAsync(member);

        return new MemberRegisterCreateResultDto
        {
            Id = member.Id.ToString(),
            Email = member.ContactInformation.FirstOrDefault(l => l.ContactType == ContactEnumType.email && l.IsPrimary)?.Address,
            Username = member.Username
        };
    }
}