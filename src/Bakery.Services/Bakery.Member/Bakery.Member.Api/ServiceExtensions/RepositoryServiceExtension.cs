public static class RepositoryServiceExtension
{
    public static void AddRepository(this IServiceCollection services) =>
        services.AddScoped<IMemberRepository, MemberRepository>();
}