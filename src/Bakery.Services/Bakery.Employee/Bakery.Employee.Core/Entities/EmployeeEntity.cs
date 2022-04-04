using System;
using Bakery.SharedKernel.Entities;
using Bakery.SharedKernel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Employee.Core.Entities;

public class EmployeeEntity : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("username")]
    public string Username { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("surname")]
    public string Surname { get; set; }
    [BsonElement("gender")]
    [BsonRepresentation(BsonType.String)]
    public GenderType Gender { get; set; }
    [BsonElement("employment_begin_date")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime EmploymentBegintDate { get; set; }
    [BsonElement("employment_end_date")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime? EmploymentEndDate { get; set; }
    [BsonElement("employee_type")]
    public string EmployeeType { get; set; }
    [BsonElement("employment_level")]
    public string EmploymentLevel { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
    public List<Contact> ContactInformations { get; set; } = new();
}