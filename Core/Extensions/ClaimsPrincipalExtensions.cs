using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // JWT ile gelen kisinin claims'lerine erismek icin dotnet de olan class .. Bunu da extends ediyor this ClaimsPrincipal claimsPrincipal
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            // ? -> null olabilir
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            // claim'leri dondurur.
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}

