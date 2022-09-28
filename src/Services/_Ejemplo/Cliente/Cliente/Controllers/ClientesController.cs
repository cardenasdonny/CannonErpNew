using Cliente.Application.Commands;
using Cliente.Application.Queries;
using Cliente.Contracts;
using Cliente.Shared.Common;
using Cliente.Shared.DataTransferObjects;
using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Cliente.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(ClienteConstants.AREA)]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteQueryService _clienteQueryService;
        private readonly ILogger<ClientesController> _logger;
        private readonly IMediator _mediator;
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public ClientesController(ILogger<ClientesController> logger, IMediator mediator, IClienteQueryService clienteQueryService, ISender sender, IPublisher publisher)
        {
            _logger = logger;
            _mediator = mediator;
            _clienteQueryService = clienteQueryService;
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ? ids = null)
        {
            
            IEnumerable<int> clientesId = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clientesId = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            var clientes = await _clienteQueryService.GetAllAsync(page, take, clientesId);

            return Ok(clientes) ;
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {            
            var cliente = await _sender.Send(new GetClienteQuery(id, TrackChanges: false));
            return Ok(cliente);            
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateCommand notification)
        {
            //await _mediator.Publish(notification);
            await _publisher.Publish(notification);
            return Ok();
        }

       
    }
}
