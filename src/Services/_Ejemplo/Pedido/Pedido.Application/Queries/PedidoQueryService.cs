using Common.Collection;
using Common.Mapping;
using Common.Paging;
using Microsoft.EntityFrameworkCore;
using Pedido.Contracts;
using Pedido.Entities.DbContexts;
using Pedido.Shared.DataTransferObjects;

namespace Pedido.Application.Queries
{
    public class PedidoQueryService : IPedidoQueryService
    {
        private readonly RepositoryContextFactory _context;

        public PedidoQueryService(
            RepositoryContextFactory context)
        {
            _context = context;
        }

        public async Task<DataCollection<PedidoDto>> GetAllAsync(int page, int take)
        {
            var collection = await _context.Pedidos
                .Include(x => x.Items)
                .OrderByDescending(x => x.PedidoId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<PedidoDto>>();
        }

        public async Task<PedidoDto> GetAsync(int id)
        {
            return (await _context.Pedidos.Include(x => x.Items).SingleAsync(x => x.PedidoId == id)).MapTo<PedidoDto>();
        }
    }
}
