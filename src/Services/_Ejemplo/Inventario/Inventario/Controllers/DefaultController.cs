using Inventario.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    // En este controlador vamos a poder utilizar el health check

    [ApiController]    
    [Route("v1/[area]/[controller]")]
    [Area(InventarioConstants.AREA)]
    public class DefaultController : ControllerBase
    {  

        private readonly ILogger<DefaultController> _logger;       

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;           
        }       

        [HttpGet]
        public IActionResult Estado()
        {
            
            return Ok("Funcionando");
        }
    }
}