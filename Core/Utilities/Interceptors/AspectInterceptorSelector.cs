using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Önce class diyor ki git class'ın attribute'lerini oku
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            // Sonra method'un attribute'larını oku(validation,log,cache,authorization,transection vb.) 
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Methodları da class'lar daki listelemeye ekle
            // Ve bunları bir listeye koy.
            classAttributes.AddRange(methodAttributes);

            // Bu attribute'lerin çalışma sırasınıda priority'ye göre sırala
            return classAttributes.OrderBy(x => x.Priority).ToArray();






            //// Otomatik olarak sistem deki bütün methodlara loglama ekler.
            //// Loglama altyapımız şimdilik hazır değil.
            // classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
        }
    }
}
