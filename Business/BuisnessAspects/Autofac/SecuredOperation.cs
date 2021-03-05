using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace Business.BuisnessAspects.Autofac
{
    // For JWT 
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        // Bu IHttpContextAccessor -> biz JWT' gondererek bir istek yapıyoruzya, buraya aynı anda binlerce kisi istek yapabilir
        // Her httprequest için bir _httpContextAccessor olusur(herkese bir tane thread olusturur).
        // using Microsoft.AspNetCore.Http installed by ManageNugetPackage
        private IHttpContextAccessor _httpContextAccessor;

        // Roller'i alıyoruz. (Attributes oldugu icin virgulle ayırarak yolluyoruz.)
        public SecuredOperation(string roles)
        {
            // Array haline getirdik.
            _roles = roles.Split(',');
            // dotnet'in Autofac ile olusturdugumuz servicemimarisine ulas -> ve .GetService
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            // Kullanıcının rollerini coz
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            // Rolleri gez ve ilgili roller var mı bak
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
