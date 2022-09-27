using Cliente.Shared.DataTransferObjects;
using Common.Collection;

namespace Cliente.Contracts
{
    public interface IClienteQueryService
    {
        Task<DataCollection<ClienteDto>> GetAllAsync(int page, int take, IEnumerable<int> clientes = null);
        Task<ClienteDto> GetByIdAsync(int id);
    }
}
