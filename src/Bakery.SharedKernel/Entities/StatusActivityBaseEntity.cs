using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public abstract class StatusActivityBaseEntity<TEnum> : BaseEntity
{
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public TEnum Status { get; set; }
    
}