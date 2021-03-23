using Base.Repository.Migrations.Enum;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Base.Repository.Helpers.Configuracoes
{
    public class MigrationsDataBase
    {
        public static void RunMigration(string conexao, EBanco banco)
        {
            IServiceProvider serviceProvider = null;
           
             serviceProvider = CreateServicesSqlserver(conexao);
         
            
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServicesSqlserver(string conexao)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(conexao)
                    .ScanIn(typeof(FluentMigrator.SqlServer.Migrations.SqlServer_00001_Database).Assembly)
                    .For.Migrations()
                    .For.EmbeddedResources()
                    )
                    .AddLogging(lb => lb.AddFluentMigratorConsole())
                    .BuildServiceProvider(false);
        }

 


        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
