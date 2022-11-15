namespace Span.Culturio.Api.Models
{
    public class CreateSubscriptionDto
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; }
    }
}
