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
            services.AddSingleton<IProductService, ProductManager>();
            services.AddSingleton<IProductDal, EfProductDal>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
