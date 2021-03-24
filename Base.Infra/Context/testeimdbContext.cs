using Base.Domain.Entidades;
using Base.Domain.Shared.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Base.Rpepository.Context
{
    public partial class testeimdbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
         = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public testeimdbContext()
        {
        }

        public testeimdbContext(DbContextOptions<testeimdbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Ator> Ator { get; set; }
        public virtual DbSet<Diretor> Diretor { get; set; }
        public virtual DbSet<Escritor> Escritor { get; set; }
        public virtual DbSet<Filme> Filme { get; set; }
        public virtual DbSet<FilmeXator> FilmeXator { get; set; }
        public virtual DbSet<FilmeXdiretor> FilmeXdiretor { get; set; }
        public virtual DbSet<FilmeXescritor> FilmeXescritor { get; set; }
        public virtual DbSet<FilmeXgenero> FilmeXgenero { get; set; }
        public virtual DbSet<FilmeXnota> FilmeXnota { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        // Unable to generate entity type for table 'dbo.VersionInfo'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
          
            modelBuilder.Entity<Ator>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Diretor>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Escritor>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.ToTable("filme");

                entity.Property(e => e.Foto)
                    .IsRequired()
                    .HasColumnName("foto")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Resumo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tempo).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<FilmeXator>(entity =>
            {
                entity.ToTable("FilmeXAtor");

                entity.HasOne(d => d.IdAtorNavigation)
                    .WithMany(p => p.FilmeXator)
                    .HasForeignKey(d => d.IdAtor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AtorF");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeXator)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmeA");
            });

            modelBuilder.Entity<FilmeXdiretor>(entity =>
            {
                entity.ToTable("FilmeXDiretor");

                entity.HasOne(d => d.IdDiretorNavigation)
                    .WithMany(p => p.FilmeXdiretor)
                    .HasForeignKey(d => d.IdDiretor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diretor");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeXdiretor)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmeD");
            });

            modelBuilder.Entity<FilmeXescritor>(entity =>
            {
                entity.ToTable("FilmeXEscritor");

                entity.HasOne(d => d.IdEscritorNavigation)
                    .WithMany(p => p.FilmeXescritor)
                    .HasForeignKey(d => d.IdEscritor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Esc");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeXescritor)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmeE");
            });

            modelBuilder.Entity<FilmeXgenero>(entity =>
            {
                entity.ToTable("FilmeXGenero");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeXgenero)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmeG");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.FilmeXgenero)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gen");
            });

            modelBuilder.Entity<FilmeXnota>(entity =>
            {
                entity.ToTable("FilmeXNota");

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeXnota)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmeN");

            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");
                //entity.Property(e => e.IdPerfil).HasColumnName("idperfil");
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(false);
                entity.Property(x => x.Administrador).HasColumnName("administrador").HasDefaultValue(false);
                //entity.Property(e => e.Nome).HasColumnName("nome");               
            });
        }
    }
}
