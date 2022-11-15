using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Api.Data;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SubscriptionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> ActivateSubscription(ActivateSubscriptionDto activateSubscriptionDto)
        {
            int id = activateSubscriptionDto.SubscriptionId;

            var subscriptionEntity = await _context.Subscriptions.FindAsync(id);

            if(subscriptionEntity is null) 
            {
                return null;
            }

            var startDate = DateTime.UtcNow;

            subscriptionEntity.ActiveFrom = startDate.ToLongDateString();
            subscriptionEntity.ActiveTo = startDate.AddMonths(1).ToLongDateString();

            _context.Subscriptions.Update(subscriptionEntity);
            await _context.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscriptionEntity);
            return subscriptionDto;
        }

        public async Task<SubscriptionDto> CreateSubscription(CreateSubscriptionDto createSubscriptionDto)
        {
            var subscriptionEntity = _mapper.Map<Data.Entity.Subscription>(createSubscriptionDto);

            _context.Subscriptions.Add(subscriptionEntity);
            await _context.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscriptionEntity);
            return subscriptionDto;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptions()
        {
            var subscriptions = await _context.Subscriptions.ToListAsync();
            var subscriptionDtos = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return subscriptionDtos;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsForUser(int userId)
        {
            var subscriptions = await _context.Subscriptions
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var subscriptionDtos = _mapper.Map<List<SubscriptionDto>> (subscriptions);
            return subscriptionDtos;
        }

        public async Task<SubscriptionDto> TrackVisit(TrackVisitDto trackVisitDto)
        {
            var subscriptionId = trackVisitDto.SubscriptionId;
            var cultureObjectId = trackVisitDto.CultureObjectId;

            var subscriptionEntity = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscriptionEntity is null) 
            {
                return null;
            }

            var packageEntity = await _context.Packages.FindAsync(subscriptionEntity.PackageId);

            if(packageEntity.CultureObjectIds.Contains(cultureObjectId) == false)
            {
                return null; //invalid culture object id
            }

            if(subscriptionEntity.isActive() == false)
            {
                return null; //inactive subscription
            }

            subscriptionEntity.RecordedVisits += 1;
            _context.Subscriptions.Update(subscriptionEntity);
            await _context.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscriptionEntity);
            return subscriptionDto;
        }
    }
}
