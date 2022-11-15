using AutoMapper;
using Span.Culturio.Api.Data.Entity;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Profiles
{
    public class CultureObjectProfile : Profile
    {
        public CultureObjectProfile() 
        {
            CreateMap<CreateCultureObjectDto, CultureObject>();
            CreateMap<CultureObject, CreateCultureObjectDto>();
            CreateMap<CultureObjectDto, CultureObject>();
            CreateMap<CultureObject, CultureObjectDto>();
            CreateMap<CultureObjectDto, CreateCultureObjectDto>();
            CreateMap<CreateCultureObjectDto, CultureObjectDto>();
        }
        
    }
}
