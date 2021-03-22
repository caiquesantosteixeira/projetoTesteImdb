using Base.Infra.Migrations.Enum;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Base.Infra.Helpers.Configuracoes
{
    public class MigrationsDataBase
    {
        public static void RunMigration(string conexao, EBanco banco)
        {
            IServiceProvider serviceProvider = null;
            switch (banco)
            {
                case EBanco.SQLSERVER:
                    serviceProvider = CreateServicesSqlserver(conexao);
                    break;
                case EBanco.POSTGRESSQL:
                    serviceProvider = CreateServicesPostgresSql(conexao);
                    break;
                default:
                    break;
            }
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

        private static IServiceProvider CreateServicesPostgresSql(string conexao)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres10_0()
                    .WithGlobalConnectionString(conexao)
                    .ScanIn(typeof(FluentMigrator.PostegreSql.Migrations.PG_00001_Database).Assembly)
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
