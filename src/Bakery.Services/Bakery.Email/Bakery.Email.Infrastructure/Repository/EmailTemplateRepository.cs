using Bakery.Email.Core.Dtos.EmailTemplateDtos;
using Bakery.Email.Core.Entities;
using Bakery.Email.Core.Repository;
using Bakery.MongoDBRepository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bakery.Email.Infrastructure.Repository;

public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
{
    public EmailTemplateRepository(IMongoClient client, IConfiguration configuration, string collectionName) : base(client, configuration, collectionName)
    {
    }

    public Task CreateEmailTemplateAsync(CreateEmailTemplateDto model)
    {
        throw new NotImplementedException();
    }

    public Task FindEmailTemplateByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EmailTemplateDto>> ListAllEmailTemplate()
    {
        throw new NotImplementedException();
    }
}
