using Cliente.Application.Commands;
using Cliente.Entities;
using Cliente.Entities.DbContexts;
using MediatR;

namespace Clientes.Application.Handlers
{
    internal sealed class ClienteCreateEventHandler  : INotificationHandler<ClienteCreateCommand> 
    {
        private readonly RepositoryContextFactory _context;        

        public ClienteCreateEventHandler(RepositoryContextFactory context)
        {
            _context = context;
        }
        public async Task Handle(ClienteCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Cliente.Entities.Cliente
            {               
                Nombre = command.Nombre,
               
            },cancellationToken);
            await _context.SaveChangesAsync();
        }

    }
}
