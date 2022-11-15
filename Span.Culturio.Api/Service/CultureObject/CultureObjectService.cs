using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Api.Data;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.CultureObject
{
    public class CultureObjectService : ICultureObjectService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CultureObjectService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CultureObjectDto> CreateCultureObject(CreateCultureObjectDto cultureObject)
        {
            var cultureObjectEntity = _mapper.Map<Data.Entity.CultureObject>(cultureObject);

            _context.CultureObjects.Add(cultureObjectEntity);
            await _context.SaveChangesAsync();

            var cultureObjectDto = _mapper.Map<CultureObjectDto>(cultureObjectEntity);
            return cultureObjectDto;
        }

        public async Task<CultureObjectDto> GetCultureObject(int id)
        {
            var cultureObject = await _context.CultureObjects.FindAsync(id);

            if(cultureObject is null)
            {
                return null;
            }

            var cultureObjectDto = _mapper.Map<CultureObjectDto>(cultureObject);

            return cultureObjectDto;

        }

        public async Task<IEnumerable<CultureObjectDto>> GetCultureObjects()
        {
            var cultureObjects = await _context.CultureObjects.ToListAsync();
            var cultureObjectDto = _mapper.Map<List<CultureObjectDto>>(cultureObjects);

            return cultureObjectDto;
        }
    }
}
