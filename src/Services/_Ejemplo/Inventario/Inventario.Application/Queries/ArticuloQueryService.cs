using Common.Collection;
using Common.Mapping;
using Common.Paging;
using Inventario.Contracts.Articulo;
using Inventario.Entities.DbContexts;
using Inventario.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Queries
{
    public class ArticuloQueryService: IArticuloQueryService
    {
        private readonly RepositoryContextFactory _context;
        public ArticuloQueryService(RepositoryContextFactory context)
        {
            _context = context;
        }
        public async Task<DataCollection<ArticuloDto>> GetAllAsync(int page, int take, IEnumerable<int> ? articulos = null)
        {
            var collection = await _context.Articulos
                .Where(x => articulos == null || articulos.Contains(x.ArticuloId))
                .OrderByDescending(x => x.ArticuloId)
                .GetPagedAsync(page, take);
            return collection.MapTo<DataCollection<ArticuloDto>>();
        }

        public async Task<ArticuloDto> GetByIdAsync(int id)
        {            
            return (await _context.Articulos.Include(x => x.Stock).SingleAsync(x => x.ArticuloId == id)).MapTo<ArticuloDto>();
        }

        
    }
}
