using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pedido.Shared.Common.Enums;

namespace Pedido.Shared.DataTransferObjects
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
    }
}
