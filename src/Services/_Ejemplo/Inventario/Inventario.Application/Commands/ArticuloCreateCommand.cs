using MediatR;

namespace Inventario.Application.Commands
{
    public class ArticuloCreateCommand: INotification
    {

        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
