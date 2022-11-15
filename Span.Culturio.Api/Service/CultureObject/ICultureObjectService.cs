using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.CultureObject
{
    public interface ICultureObjectService
    {
        Task<IEnumerable<CultureObjectDto>> GetCultureObjects();
        Task<CultureObjectDto> GetCultureObject(int id);
        Task<CultureObjectDto> CreateCultureObject(CreateCultureObjectDto cultureObject);
    }
}
