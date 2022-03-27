using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Email.Core.Entities;

public class ApiSubscriber
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("api_key")]
    public string ApiKey { get; set; }
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(StatusEnumType.passive)]
    public StatusEnumType Status { get; set; }
}