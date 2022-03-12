using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MemberEntity : BaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(StatusEnumType.passive)]
    public StatusEnumType Status { get; set; }

    [BsonElement("status_activity")]
    public List<StatusActivity> StatusActivity { get; set; } = new();

    [BsonElement("subscription_status")]
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(SubscriptionStatusType.free)]
    public SubscriptionStatusType SubscriptionStatus { get; set; }

    [BsonElement("subscription_status_activity")]
    public List<SubscriptionStatusActivity> SubscriptionStatusActivity { get; set; } = new();

    [BsonElement("profile_picture")]
    public string ProfilePicture { get; set; }

    [BsonElement("birth_date")]
    [BsonRepresentation(BsonType.DateTime)]

    public DateTime BirthDate { get; set; }
    [BsonRepresentation(BsonType.String)]
    [BsonDefaultValue(GenderType.male)]
    public GenderType Gender { get; set; }

    [BsonElement("contact_information")]
    public List<Contact> ContactInformation { get; set; } = new();

    [BsonElement("country")]
    [BsonDefaultValue("tr")]
    public string Country { get; set; }
}