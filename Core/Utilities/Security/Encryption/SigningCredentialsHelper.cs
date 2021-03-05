using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    /**
     * Bizim için JWT Servislerinin, webAPI'nin kullanabileceği JWT'larının oluşturlabilmesi icin elimizde olanlarin(key)
     * Buda bize imzalama nesnesini dönecektir.
     */
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            // ASP.NET anahtar olarak senbir tane hash işlemi yapacaksin anahtar oolarak bu key'i kullan(securityKey)
            // Şifreleme(algoritma olarak) olarak da HmacSha512'yi kullan
            // Hangi anahtar, hangi algoritma?
            // daha öncesinde sha216'yyı kullanarak şifrelenen verilerimiz var ise Hmacsha216 kullanabiliriz.
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
