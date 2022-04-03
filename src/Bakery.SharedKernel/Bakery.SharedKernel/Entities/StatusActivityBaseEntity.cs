using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.SharedKernel.Entities;

public abstract class StatusActivityBaseEntity<TEnum> : BaseEntity
{
    public StatusActivityBaseEntity(TEnum status)
    {
        Status = status;      
    }
    
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public TEnum Status { get; set; }
    
}