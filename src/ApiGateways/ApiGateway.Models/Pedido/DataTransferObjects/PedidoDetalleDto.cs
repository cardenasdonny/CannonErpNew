using ApiGateway.Models.Inventario.DataTransferObjects;

namespace ApiGateway.Models.Pedido.DataTransferObjects
{
    public class PedidoDetalleDto
    {
        public int PedidoDetalleId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        //Propiedades de navegación
        public ArticuloDto Articulo{ get; set; }
    }
}
