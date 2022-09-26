using Cliente.Application.Commands;
using Cliente.Contracts;
using Cliente.Shared.Common;
using Cliente.Shared.DataTransferObjects;
using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Controllers
{    
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(ClienteConstants.AREA)]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteQueryService _clienteQueryService;
        private readonly ILogger<ClientesController> _logger;
        private readonly IMediator _mediator;

        public ClientesController(ILogger<ClientesController> logger, IMediator mediator, IClienteQueryService clienteQueryService)
        {
            _logger = logger;
            _mediator = mediator;
            _clienteQueryService = clienteQueryService;
        }

        [HttpGet]
        public async Task<DataCollection<ClienteDto>> GetAll(int page = 1, int take = 10, string ? ids = null)
        {
            IEnumerable<int> clientes = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clientes = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await _clienteQueryService.GetAllAsync(page, take, clientes);
        }

        [HttpGet("{id}")]
        public async Task<ClienteDto> GetById(int id)
        {
            return await _clienteQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }

       
    }
}
