using Flunt.Notifications;
using Flunt.Validations;



namespace Base.Domain.DTOs
{
    public class EscritorDTO : Notifiable
    {
        public int Id { get; set; }
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
