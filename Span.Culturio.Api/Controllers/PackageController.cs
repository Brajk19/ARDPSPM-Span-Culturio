using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Span.Culturio.Api.Models;
using Span.Culturio.Api.Service.Package;

namespace Span.Culturio.Api.Controllers
{
    [Route("packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService) 
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<ActionResult<PackageDto>> GetPackages()
        {
            var packages = await _packageService.GetPackages();

            return Ok(packages);
        }

        [HttpPost]
        public async Task<ActionResult<PackageDto>> CreatePackage([FromBody] CreatePackageDto createPackageDto)
        {
            var packageDto = await _packageService.CreatePackage(createPackageDto);

            if(packageDto is null)
            {
                return BadRequest("Invalid culture object id");
            }

            return Ok(packageDto);
        }
    }
}
