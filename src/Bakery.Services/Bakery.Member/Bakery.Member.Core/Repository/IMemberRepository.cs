using Bakery.Member.Core.Dtos.Member;

public interface IMemberRepository : IRepository<MemberEntity>
{
    Task<bool> IsMemberExist(string username);
    Task<MemberRegisterCreateResultDto> Register(MemberRegisterCreateDto model);
    Task<MemberProfileDto> GetMemberProfileDto(string memberId);
    Task<MemberProfileDto> FindMember(MemberSignInDto model);
    Task<MemberProfileDto> FindMember(string username);
}