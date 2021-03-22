using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;

namespace Base.Domain.Commands.Menu
{
    public class PerfilUsuarioBotoesCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public int IdPermissoes { get; set; }
        public int IdPerfilUsuario { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract()
              .Requires()
              .IsGreaterThan(IdPermissoes, 0, "IdPermissoes", "Id Permissão inválido.")
              .IsGreaterThan(IdPerfilUsuario, 0, "IdPerfilUsuario", "Id do Perfil do usuário inválido.")
          );
        }
    }
}
