using static ApiGateway.Models.Pedido.Commons.Enums;

namespace ApiGateway.Models.Pedido.Commands
{
    public class PedidoCreateCommand
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
