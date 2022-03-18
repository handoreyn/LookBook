using Bakery.Member.Core.Repository;
using Bakery.Member.Infrastructure.Repository;

namespace Bakery.Member.Api.ServiceExtensions;

public static class RepositoryServiceExtension
{
    public static void AddRepository(this IServiceCollection services) =>
        services.AddScoped<IMemberRepository, MemberRepository>();
}