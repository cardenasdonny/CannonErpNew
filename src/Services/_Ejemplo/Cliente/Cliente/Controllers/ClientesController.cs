using Cliente.Application.Commands;
using Cliente.Contracts;
using Cliente.Shared.Common;
using Cliente.Shared.DataTransferObjects;
using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Controllers
{
    [Route("v1/[area]/[controller]")]
    [Area(ClienteConstants.AREA)]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteQueryService _clientQuerService;
        private readonly ILogger<ClientesController> _logger;
        private readonly IMediator _mediator;

        public ClientesController(
            ILogger<ClientesController> logger,
            IMediator mediator,
            IClienteQueryService clientQuerService)
        {
            _logger = logger;
            _mediator = mediator;
            _clientQuerService = clientQuerService;
        }

        [HttpGet]
        public async Task<DataCollection<ClienteDto>> GetAll(int page = 1, int take = 10, string ? ids = null)
        {
            IEnumerable<int> clients = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clients = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await _clientQuerService.GetAllAsync(page, take, clients);
        }

        [HttpGet("{id}")]
        public async Task<ClienteDto> GetById(int id)
        {
            return await _clientQuerService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
