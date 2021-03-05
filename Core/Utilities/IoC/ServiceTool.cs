using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        // dotnet' in service'lerini kullanarak onları build et.
        // Bu kod webAPI de veya Autofac'de olusturdugumuz injecttion'ları olusturabilmemize yarıyor
        // bundan boyle istedigimiz her hangi bir interface'ın karsılıgını bu tool vasıtasıyla alabiliriz.
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
