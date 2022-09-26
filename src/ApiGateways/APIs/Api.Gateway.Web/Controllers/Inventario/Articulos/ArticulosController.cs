using ApiGateway.Models.Inventario.DataTransferObjects;
using ApiGateway.Proxies;
using ApiGateway.Proxies.Inventario;
using ApiGateway.Shared.Common;
using Inventario.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.Web.Controllers.Inventario.Articulos
{
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(InventarioConstants.AREA)]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloProxy _articuloProxy;
        public ArticulosController(IArticuloProxy articuloProxy)
        {
            _articuloProxy = articuloProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ArticuloDto>> GetAll(int page, int take)
        {
            return await _articuloProxy.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ArticuloDto> GetById(int id)
        {
            return await _articuloProxy.GetByIdAsync(id);
        }
    }
}
