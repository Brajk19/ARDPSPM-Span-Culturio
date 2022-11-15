namespace Span.Culturio.Api.Models
{
    public class SubscriptionDto : CreateSubscriptionDto
    {
        public int Id { get; set; }
        public string? ActiveFrom { get; set; }
        public string? ActiveTo { get; set; }
        public int RecordedVisits { get; set; }
        public string State { 
            get
            {
                if (ActiveFrom == null)
                {
                    return "expired";
                }

                if (ActiveTo == null)
                {
                    return "expired";
                }

                var now = DateTime.Now;
                var startDate = DateTime.Parse(ActiveFrom);
                var endDate = DateTime.Parse(ActiveTo);

                if (DateTime.Compare(now, startDate) >= 0)
                {
                    if (DateTime.Compare(now, endDate) <= 0)
                    {
                        return "active";
                    }
                }

                return "expired";
            } 
        }
    }
}
