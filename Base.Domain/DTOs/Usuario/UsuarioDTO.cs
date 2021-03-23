using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.DTOS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOS.Usuario
{
    public class UsuarioDTO : Notifiable
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public int IdPerfil { get; set; }
        [Required]
        public string Nome { get; set; }

        public UsuarioDTO()
        {
            
        }       

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(UserName, 3, "UserName", "Preencha o Usuario com no mínimo 3 caracteres.")
               .IsEmail(Email, "Email", "Email Inválido")
               .IsGreaterThan(IdPerfil,0, "IdPerfil","Selecione o Perfil do Usuário.")
               .HasMinLen(Nome,3, "Nome", "Preencha o Nome com no mínimo 3 caracteres.")               
           );
        }

        public void ValidAtualizarExcluir()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(Id, 30, "Id", "Id Inválido.")              
           );
        }

        public void ValidateCadastrar()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(Senha, 4, "Senha", "Preencha a senha com no mínimo 4 caracteres.")
           );
        }
    }
}
