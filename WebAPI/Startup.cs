using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /**
             * AOP -> Örn; Biz bütün methodlarýmý loglamak istiyoruz. ILoggerService.Log() methodunu çaðýrýrýz. 
             * Bunun yerine [LogAspect] --> AOP Yani bir methodun önünde veya hata verdiðinde çalýþan kod parçacýklarýný bir AOP mimarisi ile yazýyoruz.
             * Bu methodu [Validate] -> Methodu doðrula.
             * Ürün eklenirse [RemoveCache} -> Cacheden kaldýr.
             * [Transaction} -> Hata olrusa geri al
             * [Performance] -> Çalýþma süresi 5sn geçerse beni uyar.
             * 
             * Method'un üzerine yazarsanýz methoda uygular
             * Class'ýn üzerine uygularsanýz tüm methodlara uygular.
             */
            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container
            services.AddControllers();
            // Singleton tü bellekte bir tane product manager üretiyor.
            // Singleton içerisinde bir data tutmuyorsak kullanabiliriz.
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Core.Utilities.Security.JWT
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            // Microsoft.AspNetCore.Authentication.JwtBearer in ManageNugetPackage
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // hangi yapıların sırasıyla devreye gireceğini kodluyoruz burada in Configure
            // Middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
