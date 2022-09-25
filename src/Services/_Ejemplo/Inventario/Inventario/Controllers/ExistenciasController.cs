using Inventario.Application.Commands;
using Inventario.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{    
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(InventarioConstants.AREA)]
    public class ExistenciasController : ControllerBase
    {
        private readonly ILogger<ExistenciasController> _logger;        
        private readonly IMediator _mediator;

        public ExistenciasController(ILogger<ExistenciasController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //:5140/api/Ejemplo/Existencia
        [HttpPut]
        public async Task<IActionResult> ExistenciaUpdate(ExistenciaUpdateCommand command)
        {
            await _mediator.Publish(command);
            
            return NoContent();
        }
    }
}
