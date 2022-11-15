using AutoMapper;
using Span.Culturio.Api.Data.Entity;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Profiles
{
    public class PackageProfile : Profile
    {
        public PackageProfile() 
        {
            CreateMap<CreatePackageDto, Package>();
            CreateMap<Package, CreatePackageDto>();
            CreateMap<Package, PackageDto>();
            CreateMap<PackageDto, Package>();
        }
    }
}
