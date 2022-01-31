using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXGeneroUpdateDTO : Notifiable
    {
        public int Id { get; set; }
        [Required]
        public int IdFilme { get; set; }
        [Required]
        public int IdGenero { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdFilme, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsGreaterThan(IdGenero, 0, "IdGenero", "IdGenero precisa ser maior que 0")
           );
        }
    }
}
