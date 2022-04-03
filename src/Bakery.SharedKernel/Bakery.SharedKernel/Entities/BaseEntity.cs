using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.SharedKernel.Entities;

public abstract class BaseEntity
{
    [BsonElement("create_date")]
    [BsonDefaultValue(BsonType.DateTime)]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    [BsonElement("update_date")]
    [BsonDefaultValue(BsonType.DateTime)]
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
}