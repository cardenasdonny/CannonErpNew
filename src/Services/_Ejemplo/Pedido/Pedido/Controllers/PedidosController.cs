using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedido.Application.Commands;
using Pedido.Contracts;
using Pedido.Shared.Common;
using Pedido.Shared.DataTransferObjects;

namespace Pedido.Controllers
{
    [Route("v1/[area]/[controller]")]
    [Area(PedidoConstants.AREA)]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IPedidoQueryService _pedidoQueryService;
        private readonly IMediator _mediator;

        public PedidosController(ILogger<PedidosController> logger, IPedidoQueryService  pedidoQueryService, IMediator mediator)
        {
            _logger = logger;
            _pedidoQueryService = pedidoQueryService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<PedidoDto>> GetAll(int page = 1, int take = 10)
        {
            return await _pedidoQueryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<PedidoDto> Get(int id)
        {
            return await _pedidoQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
