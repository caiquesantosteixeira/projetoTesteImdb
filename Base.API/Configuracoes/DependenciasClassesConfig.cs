using Base.Domain.Handler.Menu;
using Base.Domain.Handler.Usuario;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Repositorios.Usuario;
using Base.Infra.Repositorios.Log;
using Base.Infra.Repositorios.Menu;
using Base.Infra.Repositorios.Usuario;
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

            // Repositórios
            services.AddTransient<IUsuario, LoginRepository>();
            services.AddTransient<IPerfilUsuario, PerfilUsuarioRepository>();
            services.AddTransient<IPerfil, PerfilRepository>();
            services.AddTransient<IMenuOpcoes, MenuOpcoesRepository>();
            services.AddTransient<IPermissoes, PermissoesRepository>();
            services.AddTransient<IPerfilUsuarioBotoes, PerfilUsuarioBotoesRepository>();
            services.AddTransient<IMenu, MenuRepository>();
            services.AddTransient<IMenuOpcoesBotoes, MenuOpcoesBotoesRepository>();
            services.AddTransient<ILog, LogRepository>();


            // Handler
            services.AddTransient<UsuarioHandler, UsuarioHandler>();
            services.AddTransient<PerfilUsuarioHandler, PerfilUsuarioHandler>();
            services.AddTransient<PerfilHandler, PerfilHandler>();
            services.AddTransient<MenuOpcoesHandler, MenuOpcoesHandler>();
            services.AddTransient<PerfilUsuarioBotoesHandler, PerfilUsuarioBotoesHandler>();
            services.AddTransient<MenuHandler, MenuHandler>();
            services.AddTransient<MenuOpcoesBotoesHandler, MenuOpcoesBotoesHandler>();

            return services;
        }
    }
}
