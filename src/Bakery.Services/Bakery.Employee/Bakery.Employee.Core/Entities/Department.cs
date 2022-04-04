using Bakery.SharedKernel.Entities;
using Bakery.SharedKernel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Employee.Core.Entities;

public class Department : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public StatusEnumType Status { get; set; }
}