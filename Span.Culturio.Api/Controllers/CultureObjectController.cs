using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Span.Culturio.Api.Models;
using Span.Culturio.Api.Service.CultureObject;

namespace Span.Culturio.Api.Controllers
{
    [Route("culture-objects")]
    [ApiController]
    public class CultureObjectController : ControllerBase
    {
        private readonly ICultureObjectService _cultureObjectService;

        public CultureObjectController(ICultureObjectService cultureObjectService)
        {
            _cultureObjectService = cultureObjectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CultureObjectDto>>> GetCultureObjects()
        {
            var cultureObjects = await _cultureObjectService.GetCultureObjects();
            return Ok(cultureObjects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CultureObjectDto>> GetCultureObject(int id)
        {
            var cultureObject = await _cultureObjectService.GetCultureObject(id);

            if(cultureObject is null)
            {
                return NotFound("Culture object not found");
            }

            return Ok(cultureObject);
        }

        [HttpPost]
        public async Task<ActionResult<CultureObjectDto>> CreateCultureObject([FromBody] CreateCultureObjectDto createCultureObjectDto)
        {
            var cultureObjectDto = await _cultureObjectService.CreateCultureObject(createCultureObjectDto);
            return Ok(cultureObjectDto);
        }
    }
}
