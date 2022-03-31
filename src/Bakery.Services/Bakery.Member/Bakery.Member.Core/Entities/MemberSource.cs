using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Member.Core.Entities;

public class MemberSource : BaseEntity
{
    [BsonElement("source_url")]
    public string SourceUrl { get; set; }
    [BsonElement("utm_source")]
    public string UtmSource { get; set; }
    [BsonElement("utm_medium")]
    public string UtmMedium { get; set; }
}