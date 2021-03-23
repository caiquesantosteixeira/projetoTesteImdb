using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.DTOS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOS.Usuario
{
    public class AlterarSenhaDTO: Notifiable
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ConfirmPassword { get; set; }

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(UserName, 3, "UserName", "Preencha o Usuario com no mínimo 3 caracteres.")
               .HasMaxLen(SenhaAtual, 4, "SenhaAtual", "Preencha a Senha com no mínimo 4 caracteres.")
               .HasMaxLen(Password, 4, "Password", "Preencha a Nova Senha com no mínimo 4 caracteres.")
               .HasMaxLen(ConfirmPassword, 4, "ConfirmPassword", "Preencha a Confirmação de Senha com no mínimo 4 caracteres.")
               .AreNotEquals(ConfirmPassword, Password, "ConfirmPassword", "As senhas não conferem")               
           );
        }
    }
}
