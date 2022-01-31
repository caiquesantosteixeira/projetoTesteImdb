using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class DiretorUpdateDTO : Notifiable
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(Nome, 3, "UserName", "Preencha o Usuario com no mínimo 3 caracteres.")

           );
        }
    }


}
