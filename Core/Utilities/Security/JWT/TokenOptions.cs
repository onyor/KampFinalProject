using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    // Bu bir helperClass' tır. 
    // "Options" -> dediğimiz için tüm props'lar bir optio
    //
    // Normal de biz entity'leri tekil veririz. yani sınıfları ama bu class option olduğundan cogul veriyoruz   
    public class TokenOptions
    {
        // appsettings.json -> daki okudugumuz verileri bu nesneye atıyoruz.
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
