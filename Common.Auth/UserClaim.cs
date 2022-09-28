using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Auth
{
    public static class UserClaim
    {
        public static string GetEmail(ClaimsIdentity claims)
        {
            // Buscamos el Claim
            var usernameClaim = claims.Claims
                .Where(x => x.Type == ClaimTypes.Email)
                .FirstOrDefault();
            return usernameClaim.Value;
        }
        public static string GetName(ClaimsIdentity claims)
        {
            // Buscamos el Claim
            var usernameClaim = claims.Claims
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            return usernameClaim.Value;
        }
    }
}
