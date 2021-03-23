using Base.Domain.Entidades.Usuarios;
using Base.Domain.Shared.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Base.Repository.Context
{
    public partial class DataContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
       = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<ViewPerfilUsuario>(entity =>
            {               
                //entity.HasNoKey();
                entity.ToTable("c_perfil_usuario");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Menu).HasColumnName("menu");
                entity.Property(e => e.Permissao).HasColumnName("permissao");
                entity.Property(e => e.IdPerfil).HasColumnName("idperfil");
            });




            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");
                //entity.Property(e => e.IdPerfil).HasColumnName("idperfil");
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(false);
                entity.Property(x => x.Administrador).HasColumnName("administrador").HasDefaultValue(false);
                //entity.Property(e => e.Nome).HasColumnName("nome");               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual DbSet<ViewPerfilUsuario> ViewPerfilUsuario { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
