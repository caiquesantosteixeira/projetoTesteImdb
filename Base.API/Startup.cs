using Base.API.Configuracoes;
using Base.Repository.Context;
using Base.Repository.Helpers.Configuracoes;
using Base.Repository.Migrations.Configs;
using Base.Rpepository.Context;
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


            string conexao = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<testeimdbContext>(a => a.UseSqlServer(conexao),contextLifetime:ServiceLifetime.Transient);

            // Checa se o Banco existe/Cria antes de executar as Migrations           
            CheckDatabase.DatabaseExist(conexao);           

            // configura��o do Identity
            services.AddIdentityConfig(Configuration);

            // Inje��o de dependencias
            services.AddDependenciasConfig();

            // Adiciona as instancias de alguns servi�os
            services.AddIntanciaServiceConfig();

            //Rodas as Migra�oes do Identity
            InicializaDatabase.ExecutaIdentityMigrations();

            // Roda os Migrations
            MigrationsDataBase.RunMigration(conexao);

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


            ////Rodas as Migra�oes do Identity
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
