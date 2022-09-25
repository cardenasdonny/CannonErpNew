using MediatR;
using static Inventario.Shared.Common.Enums;

namespace Inventario.Application.Commands
{
    public class ExistenciaUpdateCommand : INotification
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
