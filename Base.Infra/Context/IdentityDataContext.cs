using Base.Domain.Shared.Entidades.Usuario;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Base.Infra.Context
{
    public class IdentityDataContext: IdentityDbContext<Usuarios>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuarios>(entity => {
                entity.ToTable("usuarios");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Ativo).HasColumnName("ativo").HasDefaultValue(false);
                entity.Property(x => x.Administrador).HasColumnName("administrador").HasDefaultValue(false);
            });
        }
    }
}
