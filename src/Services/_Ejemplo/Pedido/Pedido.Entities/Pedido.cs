using static Pedido.Shared.Common.Enums;

namespace Pedido.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public PedidoEstado PedidoEstado { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public int ClienteId { get; set; }
        public ICollection<PedidoDetalle> Items { get; set; } = new List<PedidoDetalle>();
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
    }
}
