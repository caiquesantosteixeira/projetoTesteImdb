using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXNotaInsertDTO : Notifiable
    {
        [Required]
        public int IdFilme { get; set; }
        [Required]
        public string IdUsuario { get; set; }
        [Required]
        public int Nota { get; set; }
        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdFilme, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsNotNull(IdUsuario,  "IdUsuario", "IdUsuario precisa ser maior que 0")
               .IsGreaterThan(Nota, 0, "IdFilme", "Nota precisa ser maior que 0")
               .IsLowerThan(Nota, 4, "Nota", "Nota precisa ser maior ou igual 0 e menor ou igual a 4")
           );
        }
    }
}
