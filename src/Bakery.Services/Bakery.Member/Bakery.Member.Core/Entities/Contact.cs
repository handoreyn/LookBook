using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Contact
{
    [BsonElement("contact_type")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(ContactEnumType.email)]
    public ContactEnumType ContactType { get; set; }
    [BsonElement("address")]
    public string Address { get; set; }
    [BsonElement("is_primary")]
    public bool IsPrimary { get; set; }
}