using Bakery.SharedKernel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Email.Core.Entities;

public class EmailTemplate
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("country")]
    public string Country { get; set; }
    [BsonElement("language")]
    public string Language { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("content")]
    public string Content { get; set; }
    [BsonElement("parameters")]
    public List<string> Parameters { get; set; } = new();
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(StatusEnumType.passive)]
    public StatusEnumType Status { get; set; }
}