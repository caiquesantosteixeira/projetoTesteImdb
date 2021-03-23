using Base.Domain.Shared.Entidades.Usuario;
using Base.Repository.Context;
using Base.Repository.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Util.Log;

namespace Base.Repository.Migrations.Configs
{
    public class InicializaDatabase
    {
        public static void InicializarBanco(IServiceProvider servico)
        {
            using (var serviceScope = servico.GetService<IServiceScopeFactory>().CreateScope())
            {

                var db = serviceScope.ServiceProvider.GetService<IdentityDataContext>();
                //db.Database.Migrate();
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
                if (db.Usuarios.AsNoTracking().ToList().Count == 0)
                {
                    AlimentarBaseDados(db);
                }

            }
        }

        public static void ExecutaIdentityMigrations()
        {
            var db = LocalizarService.Get<IdentityDataContext>("IdentityDataContext");
            if (db.Database.GetPendingMigrations().Count() > 0)
            {
                db.Database.Migrate();
            }
            if (db.Usuarios.AsNoTracking().ToList().Count == 0)
            {
                AlimentarBaseDados(db);
            }
        }

        public static void AlimentarBaseDados(IdentityDataContext db)
        {
            try
            {
                #region AddUsuario
                List<Usuarios> usuarios = new List<Usuarios>();

                var usu1 = new Usuarios();
                usu1.Id = "666ef5e2-b9b3-4691-8449-52c6444bb6b2";
                usu1.UserName = "jamsoft";
                usu1.NormalizedUserName = "JAMSOFT";
                usu1.Email = "scariodes1895@gmail.com";
                usu1.NormalizedEmail = "SCARIODES1895@GMAIL.COM";
                usu1.EmailConfirmed = true;
                usu1.PasswordHash = "AQAAAAEAACcQAAAAELaNWxeWj9tBjiw13tQTvOVbd7nmDhof/3Wp2CXQmyBvKO0JyuZ3vBzf3QXfTzOlIA==";
                usu1.SecurityStamp = "5X5F7RIXE5DHAIWEM4MCGM7QRFQOK67C";
                usu1.ConcurrencyStamp = "6625979f-d1c1-46fb-aa54-4e094badd8bd";
                usu1.Ativo = true;
                usu1.IdPerfil = 1;
                usu1.Nome = "Paulo Henrique";     
                usuarios.Add(usu1);

                db.Usuarios.AddRange(usuarios);
                db.SaveChanges();

                #endregion               
            }
            catch (Exception ex)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string versao = fvi.FileVersion;

                Log.FazLog("Criação das Tabelas/Banco", "Log", versao, ex);
            }

        }

    }
}
