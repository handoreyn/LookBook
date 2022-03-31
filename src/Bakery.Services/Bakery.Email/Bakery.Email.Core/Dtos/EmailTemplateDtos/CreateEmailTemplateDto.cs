namespace Bakery.Email.Core.Dtos.EmailTemplateDtos;

public class CreateEmailTemplateDto : DtoBase
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public IEnumerable<string> Parameters { get; set; }
    public StatusEnumType Status { get; set; }
}