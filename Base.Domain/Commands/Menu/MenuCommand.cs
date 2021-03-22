using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;

namespace Base.Domain.Commands.Menu
{
    public class MenuCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public string Menu1 { get; set; }
        public string Icone { get; set; }
        public int? Ordem { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(Menu1, 2, "Menu1", "Preencha o Menu com no mínimo 2 caractere.")
               .HasMinLen(Icone, 2, "Icone", "Preencha o Icone com no mínimo 2 caractere.")              
           );
        }

        public void ValidateAtualizar()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(Id, 0, "Id", "Id Inválido.")
           );
        }
    }
}
