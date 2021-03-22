using Base.Domain.Entidades.Menu;
using Base.Domain.Entidades.Usuarios;
using Base.Domain.Shared.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Base.Infra.Context
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

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");                

                entity.HasIndex(e => e.Id)
                    .HasName("idx_menu_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id");                 

                entity.Property(e => e.Icone)
                    .HasColumnName("icone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Menu1)
                    .HasColumnName("menu")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ordem).HasColumnName("ordem");
            });

            modelBuilder.Entity<MenuOpcoes>(entity =>
            {
                entity.ToTable("menu_opcoes");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_menu_opcoes_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id");
                   

                entity.Property(e => e.Ativo)
                    .HasColumnName("ativo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdMenu).HasColumnName("id_menu");

                entity.Property(e => e.PathUrl)
                    .IsRequired()
                    .HasColumnName("path_url")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SlugPermissao)
                    .HasColumnName("slug_permissao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubMenu).HasColumnName("submenu");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VisivelMenu)
                    .HasColumnName("visivel_menu")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MenuOpcoesBotoes>(entity =>
            {
                entity.ToTable("menu_opcoes_botoes");                

                entity.HasIndex(e => e.Id)
                    .HasName("idx_menu_opcoes_botoes_id");

                entity.HasIndex(e => e.IdMenuOpcoes)
                    .HasName("idx_menu_opcoes_botoes_id_menu_opcoes");

                entity.HasIndex(e => e.IdPermissoes)
                    .HasName("idx_menu_opcoes_botoes_id_permissoes");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IdMenuOpcoes).HasColumnName("id_menu_opcoes");

                entity.Property(e => e.IdPermissoes).HasColumnName("id_permissoes");

                entity.HasOne(d => d.IdMenuOpcoesNavigation)
                    .WithMany(p => p.MenuOpcoesBotoes)
                    .HasForeignKey(d => d.IdMenuOpcoes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_menu_opcoes_botoes_menu_opcoes");

                entity.HasOne(d => d.IdPermissoesNavigation)
                    .WithMany(p => p.MenuOpcoesBotoes)
                    .HasForeignKey(d => d.IdPermissoes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_menu_opcoes_botoes_permissoes");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("modulo");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_modulo_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id");                    

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModuloMenuOpcoes>(entity =>
            {
                entity.ToTable("modulo_menu_opcoes");

                entity.HasIndex(e => e.IdMenuOpcoes)
                    .HasName("idx_modulo_menu_opcoes_id_menu_opcoes");

                entity.HasIndex(e => e.IdModulo)
                    .HasName("idx_modulo_menu_opcoes_id_modulo");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IdMenuOpcoes).HasColumnName("id_menu_opcoes");

                entity.Property(e => e.IdModulo).HasColumnName("id_modulo");

                entity.HasOne(d => d.IdMenuOpcoesNavigation)
                    .WithMany(p => p.ModuloMenuOpcoes)
                    .HasForeignKey(d => d.IdMenuOpcoes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_modulo_menu_opcoes_menu_opcoes");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.ModuloMenuOpcoes)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_modulo_menu_opcoes_modulo");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("perfil");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_perfil_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PerfilUsuario>(entity =>
            {
                entity.ToTable("perfil_usuario");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_perfil_usuario_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IdModuloMenuOpc).HasColumnName("id_modulo_menu_opc");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.HasOne(d => d.IdModuloMenuOpcNavigation)
                    .WithMany(p => p.PerfilUsuario)
                    .HasForeignKey(d => d.IdModuloMenuOpc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_perfil_usuario_modulo_menu_opcoes");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.PerfilUsuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_perfil_usuario_perfil");
            });

            modelBuilder.Entity<PerfilUsuarioBotoes>(entity =>
            {
                entity.ToTable("perfil_usuario_botoes");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_perfil_usuario_botoes_id")
                    .IsUnique();

                entity.HasIndex(e => e.IdPerfilUsuario)
                    .HasName("idx_perfil_usuario_botoes_id_perfil_usuario");

                entity.HasIndex(e => e.IdPermissoes)
                    .HasName("idx_perfil_usuario_botoes_id_permissoes");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IdPerfilUsuario).HasColumnName("id_perfil_usuario");

                entity.Property(e => e.IdPermissoes).HasColumnName("id_permissoes");

                entity.HasOne(d => d.IdPerfilUsuarioNavigation)
                    .WithMany(p => p.PerfilUsuarioBotoes)
                    .HasForeignKey(d => d.IdPerfilUsuario)
                    .HasConstraintName("fk_perfil_usuario_botoes_id_perfil_usuario");

                entity.HasOne(d => d.IdPermissoesNavigation)
                    .WithMany(p => p.PerfilUsuarioBotoes)
                    .HasForeignKey(d => d.IdPermissoes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_perfil_usuario_botoes_id_permissoes");
            });

            modelBuilder.Entity<Permissoes>(entity =>
            {
                entity.ToTable("permissoes");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_permissoes_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TypeCampo)
                    .IsRequired()
                    .HasColumnName("type_campo")
                    .HasMaxLength(10)
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual DbSet<ViewPerfilUsuario> ViewPerfilUsuario { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }        
        public virtual DbSet<MenuOpcoes> MenuOpcoes { get; set; }
        public virtual DbSet<MenuOpcoesBotoes> MenuOpcoesBotoes { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<ModuloMenuOpcoes> ModuloMenuOpcoes { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public virtual DbSet<PerfilUsuarioBotoes> PerfilUsuarioBotoes { get; set; }
        public virtual DbSet<Permissoes> Permissoes { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
