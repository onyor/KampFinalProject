using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        // appsettings.json dosyasındaki verileri okumamıza yarar
        public IConfiguration Configuration { get; }
        // Okudugumuz bu verileri(degerleri) bir nesneye atmak icin bir nesne olusturuyoruz.
        private TokenOptions _tokenOptions;
        // AccessToken ne zaman gecersizlesicek.
        private DateTime _accessTokenExpiration;

        // Constructor bloguna injecct ediyoruz.
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            // https://json.schemastore.org/appsettings  -> "TokenOptions"
            // Configurations' daki alanı bul(GetSection(hangi alan -> "TokenOptions")) ve bu degerli Get<TokenOptions> bu sınıfını kulllanarak  map'liyoruz.
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            // Bu token ne zaman bitecek.  _tokenOptions -> daki configuration'dan alıyor
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            // CreateSecuriityKey(_tokenOptions daki key degerini verip) istenilen formata donusturuyoruz.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            // Hangi anahtari ve algoritmai kullanacagiz 
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            // Ve ortaya bir JWT uretiyoruz.(_tokenOptions, kullaiciBilgisi, bu adamin claim'leri neler.)  -- Bu bilgiler ile methoda ilerleyelim(below)
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            // 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        // using System.IdentityModel.Tokens.Jwt installed by ManageNugetPackage
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            // JWT'ı burada olusturuyoruz.
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                // notBefore -> Suandan onceki bir deger verilemez
                notBefore: DateTime.Now,
                // Goto -> SetClaims
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        // Bir Claim listesi olusturuyoruz.
        /**
         * Claim -> Yetki'dir ama ayni zamanda daha fazlasidir.
         * Bir JWT icerisinde baska bilgilerde olabilir, bunlar aslında claim dedigimiz JWT kullaniciya karsilik gelen (Id,Email,First/Lastname,Roller)
         */
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            // Ilgili claim bilgileri
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            // Veri tabanından cektigimiz rolleri
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
            /**
             * dotnet de var olan bir nesneye yeni method'lar ekleyebiliriz.
             * Örn: JwTSecurityToken bize ait değil ve biz buraya yeni method'lar ekleyebiliyoruz. ve bu işleme extensions deniyor.
             *
             * claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); (bu kodu buraya yazmak yerine extensions yazıyoruz)
             */
        }
    }
}
