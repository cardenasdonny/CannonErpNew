using MediatR;
using static Pedido.Shared.Common.Enums;

namespace Pedido.Proxies.Inventario.Commands
{
    public class ExistenciaUpdateCommand 
    {
        public IEnumerable<ExistenciaUpdateItem> Items { get; set; } = new List<ExistenciaUpdateItem>();
    }

    public class ExistenciaUpdateItem
    {
        public int ArticuloId { get; set; }
        public int Stock { get; set; }
        public ExistenciaAction Action { get; set; }
    }
}
