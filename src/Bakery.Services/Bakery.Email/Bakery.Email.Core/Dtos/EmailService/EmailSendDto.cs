using System.Text.Json.Serialization;

namespace Bakery.Email.Core.Dtos.EmailService
{
    public class EmailSendDto
    {
        [JsonPropertyName("personalizations")]
        public List<Personalization> Personalizations { get; set; }

        [JsonPropertyName("from")]
        public From From { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("content")]
        public List<Content> Content { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class From
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Personalization
    {
        [JsonPropertyName("to")]
        public List<From> To { get; set; }
    }
}