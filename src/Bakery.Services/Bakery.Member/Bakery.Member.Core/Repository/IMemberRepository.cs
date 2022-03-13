public interface IMemberRepository : IRepository<MemberEntity>
{
    Task<bool> IsMemberExist(string username);
    Task<MemberRegisterCreateResultDto> Register(MemberRegisterCreateDto model);
    Task<MemberProfileDto> GetMemberProfileDto(string memberId);
}