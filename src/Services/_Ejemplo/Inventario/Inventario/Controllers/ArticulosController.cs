using Common.Collection;
using Inventario.Application.Commands;
using Inventario.Contracts.Articulo;
using Inventario.Shared.Common;
using Inventario.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("v1/[area]/[controller]")]
    [Area(InventarioConstants.AREA)]
    public class ArticulosController : ControllerBase
    {
        private readonly ILogger<ArticulosController> _logger;
        private readonly IArticuloQueryService _articuloQueryService;
        private readonly IMediator _mediator;

        public ArticulosController(ILogger<ArticulosController> logger, IArticuloQueryService articuloQueryService, IMediator mediator)
        {
            _logger = logger;
            _articuloQueryService = articuloQueryService;
            _mediator = mediator;
        }

        //:5140/api/Ejemplo/Articulos
        
        [HttpGet]        
        public async Task<DataCollection<ArticuloDto>> GetAll (int page=1, int take=10, string ? ids=null)
        {
            IEnumerable<int> articulos = null;
            if (!string.IsNullOrEmpty(ids)) 
            {
                articulos = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _articuloQueryService.GetAllAsync(page,take, articulos);                
        }

        //:5140/api/Ejemplo/Articulos/1
        [HttpGet("{id}")]
        public async Task<ArticuloDto> GetById(int id)
        {            
            return await _articuloQueryService.GetByIdAsync(id);
        }

        //:5140/api/Ejemplo/Articulos
        [HttpPost]
        public async Task<IActionResult> Create(ArticuloCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
