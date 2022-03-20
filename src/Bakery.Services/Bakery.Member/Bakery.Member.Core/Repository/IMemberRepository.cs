using Bakery.Member.Core.Dtos.Member;
using Bakery.Member.Core.Entities;

namespace Bakery.Member.Core.Repository;

public interface IMemberRepository : IRepository<MemberEntity>
{
    Task<bool> IsMemberExist(string username);
    Task<MemberRegisterCreateResultDto> Register(MemberRegisterCreateDto model);
    Task<MemberProfileDto> GetMemberProfileDto(string memberId);
    Task<MemberProfileDto> FindMember(MemberSignInDto model);
    Task<MemberProfileDto> FindMember(string username);
    Task Subscribe(string memberId);
}