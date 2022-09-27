using Cliente.Contracts;
using Cliente.Entities.DbContexts;
using Cliente.Shared.DataTransferObjects;
using Common.Collection;
using Common.Mapping;
using Common.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Queries
{
    public class ClienteQueryService : IClienteQueryService
    {
        private readonly RepositoryContextFactory _context;

        public ClienteQueryService(
            RepositoryContextFactory context)
        {
            _context = context;
        }

        public async Task<DataCollection<ClienteDto>> GetAllAsync(int page, int take, IEnumerable<int> clientes = null)
        {
            var collection = await _context.Clientes
                .Where(x => clientes == null || clientes.Contains(x.ClienteId))
                .OrderBy(x => x.Nombre)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClienteDto>>();
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            return (await _context.Clientes.SingleAsync(x => x.ClienteId == id)).MapTo<ClienteDto>();
        }
    }
}
