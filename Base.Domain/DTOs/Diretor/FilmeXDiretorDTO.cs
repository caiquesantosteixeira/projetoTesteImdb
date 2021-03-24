using Flunt.Notifications;
using Flunt.Validations;


namespace Base.Domain.DTOs
{
    public class FilmeXDiretorDTO : Notifiable
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdDiretor { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(IdFilme, 0, "IdFilme", "IdFilme precisa ser maior que 0")
               .IsGreaterThan(IdDiretor, 0, "IdDiretor", "IdAtor precisa ser maior que 0")
           );
        }
    }
}
