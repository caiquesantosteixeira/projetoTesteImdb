using Base.Domain.Shared.Entidades.Usuario;
using Base.Domain.ValueObject.Config;
using Base.Repository.Context;
using Base.Repository.Helpers.Extensoes;
using Base.Repository.Migrations.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Util.HelpersFuncoes;

namespace Base.API.Configuracoes
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            string conexao = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityDataContext>(a => a.UseSqlServer(conexao));

            services.AddDefaultIdentity<Usuarios>(opt => 
                    {
                        opt.Password.RequireDigit = false;
                        opt.Password.RequireNonAlphanumeric = false;
                        opt.Password.RequireLowercase = false;
                        opt.Password.RequireUppercase = false;
                        opt.Password.RequiredLength = 4;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDataContext>()
                    .AddErrorDescriber<IdentityMensagensPortugues>()
                    .AddDefaultTokenProviders();

            // JWT
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor                    
                };
            });



            return services;
        }
    }
}
