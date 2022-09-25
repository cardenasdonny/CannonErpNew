using Inventario.Application.Commands;
using Inventario.Entities;
using Inventario.Entities.DbContexts;
using MediatR;


namespace Inventario.Application.Handlers
{
    public class ArticuloCreateEventHandler
        :INotificationHandler<ArticuloCreateCommand>
    {
        private readonly RepositoryContextFactory _context;
        public ArticuloCreateEventHandler(RepositoryContextFactory context)
        {
            _context = context;
        }
        public async Task Handle(ArticuloCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Articulo
            {
                Nombre = command.Nombre,
                Precio = command.Precio,
            }, cancellationToken);
            await _context.SaveChangesAsync();
        }
       
    }
}
