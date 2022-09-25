namespace Pedido.Entities
{
    public class PedidoDetalle
    {
        public int PedidoDetalleId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
