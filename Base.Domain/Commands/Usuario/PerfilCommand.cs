using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Entidades.Menu;

namespace Base.Domain.Commands.Usuario
{
    public class PerfilCommand : Notifiable, ICommand
    {
        public PerfilCommand()
        {

        }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()               
               .HasMinLen(Nome, 1, "Nome", "Preencha o Nome do Perfil")
           );
        }

        public void ValidateUpdate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(Id, 0, "Id", "Id do Perfil inválido")
           );
        }

        public int Id { get; set; }
        public string Nome { get; set; }       
    }
}
