using Bakery.Email.Core.Dtos.EmailTemplateDtos;
using Bakery.Email.Core.Entities;
using Bakery.MongoDBRepository;

namespace Bakery.Email.Core.Repository;

public interface IEmailTemplateRepository : IRepository<EmailTemplate>
{
    Task CreateEmailTemplateAsync(CreateEmailTemplateDto model);
    Task FindEmailTemplateByIdAsync(string id);
    Task<IEnumerable<EmailTemplateDto>> ListAllEmailTemplate();
}