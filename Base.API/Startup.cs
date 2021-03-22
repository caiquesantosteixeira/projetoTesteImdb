using Base.API.Configuracoes;
using Base.Infra.Context;
using Base.Infra.Helpers.Configuracoes;
using Base.Infra.Migrations.Configs;
using Base.Infra.Migrations.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Util.HelpersFuncoes;

namespace Base.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var tipoBanco = EBanco.SQLSERVER; 
            
            var conexao = Configuration.GetConnectionString(tipoBanco.GetEnumDescription());             
            services.AddDbContext<DataContext>(opt => 
            {
                if (EBanco.SQLSERVER == tipoBanco)
                {
                    opt.UseSqlServer(conexao, b => {
                        b.MigrationsAssembly("Base.Infra");
                        b.UseRowNumberForPaging();
                    });
                }
                else
                {
                    opt.UseNpgsql(conexao, b => b.MigrationsAssembly("Base.Infra"));
                }             
            });            

            // Checa se o Banco existe/Cria antes de executar as Migrations           
            CheckDatabase.DatabaseExist(conexao, tipoBanco);           

            // configuração do Identity
            services.AddIdentityConfig(Configuration, tipoBanco);

            // Injeção de dependencias
            services.AddDependenciasConfig();

            // Adiciona as instancias de alguns serviços
            services.AddIntanciaServiceConfig();

            //Rodas as Migraçoes do Identity
            InicializaDatabase.ExecutaIdentityMigrations();

            // Roda os Migrations
            MigrationsDataBase.RunMigration(conexao, tipoBanco);

            services.AddCors(o => o.AddPolicy("EnableCors", builder => {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            //Swagger
            services.ConfiguraSwagger();

            services.AddMvc();           
            services.AddControllers()
                    .AddNewtonsoftJson();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Projeto Base");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "index.html");
            app.UseRewriter(option);


            ////Rodas as Migraçoes do Identity
            //InicializaDatabase.InicializarBanco(app.ApplicationServices);

            var cultureInfo = new CultureInfo("pt-BR");
            cultureInfo.NumberFormat.CurrencySymbol = "R$";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRouting();

            app.UseCors("EnableCors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
