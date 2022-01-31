using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class EscritorInsertDTO : Notifiable
    {
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
