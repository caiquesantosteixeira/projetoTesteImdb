using Flunt.Notifications;
using Flunt.Validations;


namespace Base.Domain.DTOs
{
    public class FilmeXEscritorDTO : Notifiable
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdEscritor { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdFilme, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsGreaterThan(IdEscritor, 0, "IdEscritor", "IdAtor precisa ser maior que 0")
           );
        }
    }
}
