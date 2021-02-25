using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;


// Autofac -> Core katmanına da kurulacak
namespace Core.Utilities.Interceptors
{

    // Class'lara veya methodlara ekleyebiliriz ve bir den fazla eklenebilsin ayrıca  inherit edilen ortamlarda da kullanılabilir.

    // Neden 2 defa aynı attribute' ü kullanırız. Veri tabanına loglarken yada doyaya loglarken kullanıyoruz diyelim.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    // IInterceptor -> Autofac'in interceptor özelliği var.
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        // Öncelik sıralaması önce Loglama, sonra Validation gibi
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
