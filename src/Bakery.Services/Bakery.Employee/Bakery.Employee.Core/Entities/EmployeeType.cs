using Bakery.SharedKernel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Employee.Core.Entities;

public class EmployeeType : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
}