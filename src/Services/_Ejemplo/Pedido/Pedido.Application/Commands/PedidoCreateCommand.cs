using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pedido.Shared.Common.Enums;

namespace Pedido.Application.Commands
{
    public class PedidoCreateCommand : INotification
    {
        public MetodoPago MetodoPago { get; set; }
        public int ClienteId { get; set; }
        public IEnumerable<PedidoDetalleCreateCommand> Items { get; set; } = new List<PedidoDetalleCreateCommand>();
    }

    public class PedidoDetalleCreateCommand
    {
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
