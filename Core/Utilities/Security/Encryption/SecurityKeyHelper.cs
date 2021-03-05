using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

// Microsoft.IdentityModel.Tokens added in Core from by NugetPackageManager

namespace Core.Utilities.Security.Encryption
{
    /**
     * Şifreleme sistemleri
     * Sistemler de bizim herşeyi bir byte array formatında vermemiz gerekli
     *
     * ASP.NET'in JsonWebToken servislerinin anlayacagi hale getirmemiz gerekiyor
     */
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            // Byte'ını alıp onun bir SymmetricSecurityKey haline getiriyor
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
