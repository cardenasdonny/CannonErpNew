using Common.Collection;
using Inventario.Shared.DataTransferObjects;

namespace Inventario.Contracts.Articulo
{
    public interface IArticuloQueryService
    {
        Task<DataCollection<ArticuloDto>> GetAllAsync(int page, int take, IEnumerable<int>? articulos = null);
        Task<ArticuloDto> GetByIdAsync(int id);
    }
}
