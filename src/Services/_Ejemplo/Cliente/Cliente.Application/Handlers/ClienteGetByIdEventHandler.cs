using Cliente.Application.Queries;
using Cliente.Entities.DbContexts;
using Cliente.Shared.DataTransferObjects;
using Common.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Application.Handlers
{
    public class ClienteGetByIdEventHandler: IRequestHandler<GetClienteQuery, ClienteDto>
    {
        private readonly RepositoryContextFactory _context;

        public ClienteGetByIdEventHandler(RepositoryContextFactory context)
        {
            _context = context;
        }
        public async Task<ClienteDto> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            if(request.TrackChanges)
                return (await _context.Clientes.SingleAsync(x => x.ClienteId == request.Id)).MapTo<ClienteDto>();
            else
                return (await _context.Clientes.AsNoTracking().SingleAsync(x => x.ClienteId == request.Id)).MapTo<ClienteDto>();
        }

    }
}
