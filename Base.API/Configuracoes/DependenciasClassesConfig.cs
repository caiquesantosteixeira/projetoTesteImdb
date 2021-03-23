using Base.Domain.Repositorios.Logging;
using Base.Repository.Repositorios.Log;
using Base.Repository.Repositorios.Usuario;
using Base.Service.Contracts.Usuario;
using Base.Service.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Base.API.Configuracoes
{
    public static class DependenciasClassesConfig
    {
        public static IServiceCollection AddDependenciasConfig(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserIdentity, UserIdentity>();
            services.AddTransient<IUsuario, LoginRepository>();
            services.AddTransient<ILog, LogRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            return services;
        }
    }
}
