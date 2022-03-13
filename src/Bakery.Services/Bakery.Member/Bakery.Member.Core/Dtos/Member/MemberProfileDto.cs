public class MemberProfileDto : DtoBase
{
    public string MemberId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreateDate { get; set; }
    public SubscriptionStatusType SubscriptionStatus { get; set; }
    public string Country { get; set; }
    public string ProfilePictureUrl { get; set; }
}