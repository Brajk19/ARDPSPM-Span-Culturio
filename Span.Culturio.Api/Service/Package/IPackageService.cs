using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.Package
{
    public interface IPackageService
    {
        Task<PackageDto> CreatePackage(CreatePackageDto packageDto);
        Task<IEnumerable<PackageDto>> GetPackages();
    }
}
