using Cliente.Application.Queries;
using Cliente.Entities.DbContexts;
using Cliente.Shared.DataTransferObjects;
using Common.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace Cliente.Application.Handlers
{
    public class ClienteGetByIdEventHandler: IRequestHandler<GetClienteQuery, ClienteDto>
    {
        private readonly RepositoryContextFactory _context;
        private readonly IPrincipal _principal;

        public ClienteGetByIdEventHandler(RepositoryContextFactory context, IPrincipal principal)
        {
            _context = context;
            _principal = principal;
        }
        public async Task<ClienteDto> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            if (request.TrackChanges)
                return (await _context.Clientes.SingleAsync(x => x.ClienteId == request.Id)).MapTo<ClienteDto>();
            else
                return (await _context.Clientes.AsNoTracking().SingleAsync(x => x.ClienteId == request.Id)).MapTo<ClienteDto>();
        }

    }
}
