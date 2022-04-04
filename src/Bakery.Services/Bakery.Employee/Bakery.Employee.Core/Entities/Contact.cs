using Bakery.SharedKernel.Entities;
using Bakery.SharedKernel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Employee.Core.Entities;

public class Contact : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("contact_type")]
    [BsonRepresentation(BsonType.String)]
    public ContactEnumType ContactType { get; set; }
    [BsonElement("address")]
    public string Address { get; set; }
    [BsonElement("is_primary")]
    public bool IsPrimary { get; set; }
    [BsonElement("is_corporate")]
    public bool IsCorporate { get; set; }
}