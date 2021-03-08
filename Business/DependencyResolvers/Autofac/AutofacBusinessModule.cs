using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Security.JWT;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /**
             * services.AddSingleton<IProductService, ProductManager>();
             * aynı'sı
             * IProductService istenilirse ona -> ProductManager verir.
             */
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            
            /**
             * services.AddSingleton<IProductDal, EfProductDal>();
             */
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();


            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            // Autofact bize aynı zamanda interceptor özelliği veriyor.   
            // Autofac bizim bütün sınıflarımız için ilk önce bu kod satırlarını çalıştırıyor. (Bizim Aspect'imiz(Attributes) varmı?)

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Implemente edilmiş interface'leri bul onlara onlar için AspectInterceptorSelecter'ı çağır
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
