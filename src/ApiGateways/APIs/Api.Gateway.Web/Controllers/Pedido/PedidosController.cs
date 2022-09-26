using ApiGateway.Models.Pedido.Commands;
using ApiGateway.Models.Pedido.DataTransferObjects;
using ApiGateway.Proxies.Cliente;
using ApiGateway.Proxies.Inventario;
using ApiGateway.Proxies.Pedido;
using ApiGateway.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedido.Shared.Common;

namespace Api.Gateway.Web.Controllers.Pedido
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(PedidoConstants.AREA)]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoProxy _pedidoProxy;
        private readonly IClienteProxy _clienteProxy;
        private readonly IArticuloProxy _inventarioProxy;

        public PedidosController(IPedidoProxy pedidoProxy, IClienteProxy  clienteProxy, IArticuloProxy  articuloProxy
        )
        {
            _pedidoProxy = pedidoProxy;
            _clienteProxy = clienteProxy;
            _inventarioProxy = articuloProxy;
        }

        /// <summary>
        /// Este método no necesita traer la información de los articulos porque lo usaremos solo para mostrar
        /// las cabeceras en el listado. 
        /// </summary>
        /// <param name="page">Páginas a tomar</param>
        /// <param name="take">Cantidad de registros a tomar</param>
        /// <returns></returns>
        [HttpGet]

        public async Task<DataCollection<PedidoDto>> GetAll(int page, int take)
        {
            var result = await _pedidoProxy.GetAllAsync(page, take);

            if (result.HasItems)
            {
                // Retrieve client ids
                var clienteIds = result.Items
                    .Select(x => x.ClienteId)
                    .GroupBy(g => g)
                    .Select(x => x.Key).ToList();

                var clientes = await _clienteProxy.GetAllAsync(1, clienteIds.Count(), clienteIds);

                foreach (var pedido in result.Items)
                {
                    pedido.Cliente = clientes.Items.Single(x => x.ClienteId == pedido.ClienteId);
                }
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<PedidoDto> GetById(int id)
        {
            // Traemos el pedido
            var pedido = await _pedidoProxy.GetByIdAsync(id);

            // Traemos el cliente
            pedido.Cliente = await _clienteProxy.GetByIdAsync(pedido.ClienteId);

            // Traemos los ids de los articulos que se encuentran en el pedido
            var articuloIds = pedido.Items
                .Select(x => x.ArticuloId)
                .GroupBy(g => g)
                .Select(x => x.Key).ToList();

            // Traemos los articulos (ya con todas las propiedades necesarias) del pedido
            var articulos = await _inventarioProxy.GetAllAsync(1, articuloIds.Count(), articuloIds);

            // Agregamos los articulos al pedido
            foreach (var item in pedido.Items)
            {
                item.Articulo = articulos.Items.Single(x => x.ArticuloId == item.ArticuloId);
            }

            return pedido;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoCreateCommand command)
        {
            await _pedidoProxy.CreateAsync(command);
            return Ok();
        }
    }
}
