using ApiGateway.Models.Cliente.DataTransferObjects;
using ApiGateway.Proxies.Cliente;
using ApiGateway.Shared.Common;
using Cliente.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.Web.Controllers.Clientes
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(ClienteConstants.AREA)]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteProxy _clienteProxy;

        public ClientesController(IClienteProxy clienteProxy)
        {
            _clienteProxy = clienteProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ClienteDto>> GetAll(int page, int take, string? ids = null)
        {
            IEnumerable<int> clientes = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clientes = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _clienteProxy.GetAllAsync(page, take, clientes);
        }


        [HttpGet("{id}")]
        public async Task<ClienteDto> GetById(int id)
        {
            return await _clienteProxy.GetByIdAsync(id);
        }
    }
}
