using Base.Domain.Repositorios.Logging;
using Base.Repository.Contracts;
using Base.Repository.Repositorios.Log;
using Base.Repository.Repositorios.Usuario;
using Base.Service.Contracts;
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

            services.AddTransient<IEscritorService, EscritorService>();
            services.AddTransient<IGeneroService, GeneroService>();
            services.AddTransient<IAtorService, AtorService>();
            services.AddTransient<IDiretorService, DiretorService>();
            services.AddTransient<IFilmeService, FilmeService>();

            services.AddTransient<IEscritor, EscritorRepository>();
            services.AddTransient<IGenero, GeneroRepository>();
            services.AddTransient<IAtor, AtorRepository>();
            services.AddTransient<IDiretor, DiretorRepository>();
            services.AddTransient<IFilme, FilmeRepository>();
            return services;
        }
    }
}
