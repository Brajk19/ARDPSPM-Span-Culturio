using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Span.Culturio.Api.Models;
using Span.Culturio.Api.Service.Subscription;

namespace Span.Culturio.Api.Controllers
{
    [Route("subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionDto>>> GetSubscriptions([FromQuery] int? userId = null)
        {
            IEnumerable<SubscriptionDto> subscriptionDtos;

            if(userId is null)
            {
                subscriptionDtos = await _subscriptionService.GetSubscriptions();
            }
            else
            {
                subscriptionDtos = await _subscriptionService.GetSubscriptionsForUser((int)userId);
            }

            return Ok(subscriptionDtos);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> CreateSubscription([FromBody] CreateSubscriptionDto createSubscriptionDto)
        {
            var subscriptionDto = await _subscriptionService.CreateSubscription(createSubscriptionDto);
            return Ok(subscriptionDto);
        }

        [HttpPost("activate")]
        public async Task<ActionResult<SubscriptionDto>> ActivateSubscription([FromBody] ActivateSubscriptionDto activateSubscriptionDto) 
        {
            var subscriptionDto = await _subscriptionService.ActivateSubscription(activateSubscriptionDto);
            
            if(subscriptionDto is null)
            {
                return NotFound();
            }

            return Ok(subscriptionDto);
        }

        [HttpPost("track-visit")]
        public async Task<ActionResult<SubscriptionDto>> TrackVisit([FromBody] TrackVisitDto trackVisitDto)
        {
            var subscriptionDto = await _subscriptionService.TrackVisit(trackVisitDto);

            if(subscriptionDto is null)
            {
                return BadRequest();
            }

            return Ok(subscriptionDto);
        }
    }
}
