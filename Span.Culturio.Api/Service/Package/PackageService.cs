using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Api.Data;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.Package
{
    public class PackageService : IPackageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PackageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PackageDto> CreatePackage(CreatePackageDto packageDto)
        {
            foreach(int cultureObjectId in packageDto.CultureObjectIds)
            {
                var cultureObject = await _context.CultureObjects.FindAsync(cultureObjectId);

                if(cultureObject is null)
                {
                    return null; //invalid culture object id
                }
            }

            var packageEntity = _mapper.Map<Data.Entity.Package>(packageDto);

            _context.Packages.Add(packageEntity);
            await _context.SaveChangesAsync();

            var package = _mapper.Map<PackageDto>(packageEntity);

            return package;
        }

        public async Task<IEnumerable<PackageDto>> GetPackages()
        {
            var packages = await _context.Packages.ToListAsync();
            var packageDtos = _mapper.Map<List<PackageDto>>(packages);

            return packageDtos;
        }
    }
}
