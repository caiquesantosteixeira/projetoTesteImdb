using Flunt.Notifications;
using Flunt.Validations;


namespace Base.Domain.DTOs
{
    public class FilmeXNotaDTO : Notifiable
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public string IdUsuario { get; set; }

        public int Nota { get; set; }
        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdFilme, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsNotNull(IdUsuario,  "IdUsuario", "IdUsuario precisa ser maior que 0")
               .IsGreaterThan(Nota, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsLowerThan(Nota, 4, "Nota", "Nota precisa ser maior ou igual 0 e menor ou igual a 4")
           );
        }
    }
}
