using Microsoft.Extensions.Configuration;

public class MemberRepository : RepositoryBase<MemberEntity>, IMemberRepository
{
    public MemberRepository(IConfiguration configuration) : base(configuration)
    {
    }
}