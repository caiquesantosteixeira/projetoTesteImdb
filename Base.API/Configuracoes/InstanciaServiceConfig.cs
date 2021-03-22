using Base.Domain.Repositorios.Logging;
using Base.Infra.Context;
using Base.Infra.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Base.API.Configuracoes
{
    public static class InstanciaServiceConfig
    {
        public static IServiceCollection AddIntanciaServiceConfig(this IServiceCollection services)
        {
           
            var serviceProvider = services.BuildServiceProvider();
            var serv1 = serviceProvider.GetService<DataContext>();
            LocalizarService.Registrar<DataContext>("DataContext", serv1);

            var serv2 = serviceProvider.GetService<ILog>();
            LocalizarService.Registrar<ILog>("ILog", serv2);

            var serv3 = serviceProvider.GetService<IdentityDataContext>();
            LocalizarService.Registrar<IdentityDataContext>("IdentityDataContext", serv3);           

            return services;
        }
    }
}
