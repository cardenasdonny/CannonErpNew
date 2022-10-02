using ApiGateway.Models.Cliente.DataTransferObjects;
using static ApiGateway.Models.Pedido.Commons.Enums;

namespace ApiGateway.Models.Pedido.DataTransferObjects
{
    public class PedidoDto
    {
        public int PedidoId { get; set; }
        public string PedidoNumero { get; set; }
        public PedidoEstado PedidoEstado { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public int ClienteId { get; set; }
        public IEnumerable<PedidoDetalleDto> Items { get; set; } = new List<PedidoDetalleDto>();
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }

        //Propiedades de navegación

        public ClienteDto Cliente { get; set; }
    }
}
