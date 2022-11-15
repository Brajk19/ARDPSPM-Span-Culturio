using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.Subscription
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetSubscriptions();
        Task<SubscriptionDto> CreateSubscription(CreateSubscriptionDto createSubscriptionDto);
        Task<SubscriptionDto> TrackVisit(TrackVisitDto trackVisitDto);
        Task<SubscriptionDto> ActivateSubscription(ActivateSubscriptionDto activateSubscriptionDto);
        Task<IEnumerable<SubscriptionDto>> GetSubscriptionsForUser(int userId);
    }
}
