using AutoMapper;
using Span.Culturio.Api.Data.Entity;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Profiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile() 
        {
            CreateMap<CreateSubscriptionDto, Subscription>();
            CreateMap<Subscription, CreateSubscriptionDto>();
            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<SubscriptionDto, Subscription>();
        }
    }
}
