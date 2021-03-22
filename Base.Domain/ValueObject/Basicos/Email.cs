using Flunt.Notifications;
using Flunt.Validations;

namespace Base.Domain.ValueObject.Basicos
{
    public class Email: Notifiable
    {
        public string Endereco { get; private set; }

        public Email(string email)
        {
            Endereco = email;

            Validar();
        }

        public void Validar()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsEmail(Endereco, "Email", "Email Inválido")              
           );
        }
    }
}
