using Common.Collection;
using Pedido.Shared.DataTransferObjects;

namespace Pedido.Contracts
{
    public interface IPedidoQueryService
    {
        Task<DataCollection<PedidoDto>> GetAllAsync(int page, int take);
        Task<PedidoDto> GetByAsync (int id);
    }
}
