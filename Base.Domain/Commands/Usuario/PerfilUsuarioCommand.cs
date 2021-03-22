using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Entidades.Menu;

namespace Base.Domain.Commands.Usuario
{
    public class PerfilUsuarioCommand: Notifiable, ICommand
    {
        public PerfilUsuarioCommand()
        {

        }
       
        public int Id { get; set; }
        public int IdModuloMenuOpc { get; set; }
        public int IdPerfil { get; set; }
        
        public string Titulo { get; set; }
        
        public int IdMenuOpcoes { get; set; }
        
        public bool VisivelMenu { get; set; }

        public void Validate()
        {
            AddNotifications(
              new Contract()
                .Requires()
                .IsGreaterThan(IdModuloMenuOpc, 0, "IdModuloMenuOpc", "O IdModuloMenuOpc precisa ser maior que Zero.")
                .IsGreaterThan(IdPerfil, 0, "IdPerfil", "O Perfil IdPerfil precisa ser maior que Zero.")
            );
        }

        public PerfilUsuario GetPerfilUsuario()
        {
            var pf = new PerfilUsuario();
            pf.IdMenuOpcoes = IdMenuOpcoes;
            pf.IdModuloMenuOpc = IdModuloMenuOpc;
            pf.IdPerfil = IdPerfil;
            pf.VisivelMenu = VisivelMenu;
            return pf;
        }
    }
}
