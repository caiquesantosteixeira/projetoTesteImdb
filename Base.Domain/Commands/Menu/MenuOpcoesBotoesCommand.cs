using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.Commands.Menu
{
    public class MenuOpcoesBotoesCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        [Required]
        public int IdPermissoes { get; set; }
        [Required]
        public int IdMenuOpcoes { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdPermissoes, 0, "IdPermissoes", "Id da Permissao inválido.")
               .IsGreaterThan(IdMenuOpcoes, 0, "IdMenuOpcoes", "Id do menu de opções inválido.")
           );
        }
    }
}
