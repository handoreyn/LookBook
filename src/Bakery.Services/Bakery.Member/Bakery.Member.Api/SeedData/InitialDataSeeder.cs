using Bakery.Member.Core.Entities;
using Bakery.Member.Core.Repository;
using Bakery.SharedKernel.Enums;
using Bogus;
using MongoDB.Driver;

namespace Bakery.Member.Api.SeedData;

public static class InitialDataSeeder
{
    public static WebApplication SeedData(this WebApplication webapp)
    {
        var logger = webapp.Services.CreateScope().ServiceProvider.GetService<ILogger>();
        var data = new Seeder().Data.Select(l => new InsertOneModel<MemberEntity>(l));
        var repo = webapp.Services.CreateScope().ServiceProvider.GetService<IMemberRepository>();
        if (repo?.Count() > 0)
        {
            logger?.LogInformation("App has data. No need to seed.");
            return webapp;
        }
        logger?.LogInformation("Data seed started.");
        repo?.BulkInsert(data);
        logger?.LogInformation("Data seed completed.");
        return webapp;
    }
}

public class Seeder
{
    public List<MemberEntity> Data { get; set; } = new();

    public Seeder()
    {


        var contactFaker = new Faker<Contact>()
            .RuleFor(l => l.ContactType, ContactEnumType.email)
            .RuleFor(l => l.IsPrimary, f => f.IndexFaker == 0)
            .RuleFor(l => l.Address, f => f.Internet.Email());

        var memberSourcesFaker = new Faker<MemberSource>()
            .RuleFor(l => l.CreateDate, f => f.Date.Past())
            .RuleFor(l => l.UtmMedium, f => f.Company.CompanyName())
            .RuleFor(l => l.UtmSource, f => f.Internet.DomainName())
            .RuleFor(l => l.SourceUrl, f => f.Internet.Url());

        var faker = new Faker<MemberEntity>("tr")
            .RuleFor(l => l.Username, f => f.Person.UserName)
            .RuleFor(l => l.Password, f => f.Internet.Password())
            .RuleFor(l => l.Status, f => f.PickRandom<StatusEnumType>())
            .RuleFor(l => l.StatusActivity, f => f.Random.EnumValues<StatusEnumType>().Select(t => new StatusActivity(t)).ToList())
            .RuleFor(l => l.SubscriptionStatus, f => f.PickRandom<SubscriptionStatusType>())
            .RuleFor(l => l.SubscriptionStatusActivity, f => f.Random.EnumValues<SubscriptionStatusType>().Select(t => new SubscriptionStatusActivity(t)).ToList())
            .RuleFor(l => l.ProfilePicture, f => f.Person.Avatar)
            .RuleFor(l => l.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(l => l.Gender, f => (GenderType)f.Person.Gender)
            .RuleFor(l => l.ContactInformation, contactFaker.GenerateBetween(1, 3))
            .RuleFor(l => l.Country, f => f.Address.Country())
            .RuleFor(l => l.MemberSources, memberSourcesFaker.GenerateBetween(1, 5))
        .Generate(100000);

        Data.AddRange(faker);
    }
}