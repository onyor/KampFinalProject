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
             * AOP -> �rn; Biz b�t�n methodlar�m� loglamak istiyoruz. ILoggerService.Log() methodunu �a��r�r�z. 
             * Bunun yerine [LogAspect] --> AOP Yani bir methodun �n�nde veya hata verdi�inde �al��an kod par�ac�klar�n� bir AOP mimarisi ile yaz�yoruz.
             * Bu methodu [Validate] -> Methodu do�rula.
             * �r�n eklenirse [RemoveCache} -> Cacheden kald�r.
             * [Transaction} -> Hata olrusa geri al
             * [Performance] -> �al��ma s�resi 5sn ge�erse beni uyar.
             * 
             * Method'un �zerine yazarsan�z methoda uygular
             * Class'�n �zerine uygularsan�z t�m methodlara uygular.
             */
            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container
            services.AddControllers();
            // Singleton t� bellekte bir tane product manager �retiyor.
            // Singleton i�erisinde bir data tutmuyorsak kullanabiliriz.
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
