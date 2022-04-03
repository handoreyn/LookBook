using Bakery.SharedKernel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Email.Core.Entities;

public class EmailUser
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("email_address")]
    public string EmailAddress { get; set; }
    [BsonElement("email_sender_name")]
    public string EmailSenderName { get; set; }
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(StatusEnumType.passive)]
    public StatusEnumType Status { get; set; }
}