using Cliente.Application.Commands;
using Cliente.Entities.DbContexts;
using Common.Auth;
using MediatR;
using System.Security.Claims;
using System.Security.Principal;

namespace Clientes.Application.Handlers
{
    internal sealed class ClienteCreateEventHandler  : INotificationHandler<ClienteCreateCommand> 
    {
        private readonly RepositoryContextFactory _context;
        private readonly IPrincipal _principal;

        public ClienteCreateEventHandler(RepositoryContextFactory context, IPrincipal principal)
        {
            _context = context;
            _principal = principal;
        }

       
        public async Task Handle(ClienteCreateCommand command, CancellationToken cancellationToken)
        {
            //Obtenemos el usuario tanto para el log como para registrarlo en la db
            //
            var userEmail = UserClaim.GetEmail(_principal.Identity as ClaimsIdentity);

            await _context.AddAsync(new Cliente.Entities.Cliente
            {               
                Nombre = command.Nombre,
               
            },cancellationToken);
            
            await _context.SaveChangesAsync();
        }

    }
}
