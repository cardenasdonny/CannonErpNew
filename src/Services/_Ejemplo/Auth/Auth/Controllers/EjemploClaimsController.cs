using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Auth.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploClaimsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetClaims()
        {
            // Lista de los claims del usuario logueado
            var claims = User.Claims;
            
            // Buscamos el Claim
            var usernameClaim = claims
                .Where(x => x.Type == ClaimTypes.Email)
                .FirstOrDefault();

            return Ok();
           
        }
    }
}
