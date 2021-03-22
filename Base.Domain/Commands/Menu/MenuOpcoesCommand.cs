using Flunt.Notifications;
using Flunt.Validations;
using Base.Domain.Commands.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.Commands.Menu
{
    public class MenuOpcoesCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public int? IdMenu { get; set; }
        [Required]
        public string PathUrl { get; set; }
        public int? SubMenu { get; set; }

        [Required]
        public string Titulo { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public bool VisivelMenu { get; set; }
        [Required]
        public string SlugPermissao { get; set; }        

        public void Validate()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .HasMinLen(PathUrl, 1, "PathUrl", "Preencha o PathUrl com no mínimo 1 caractere.")
               .HasMinLen(Titulo, 2, "Titulo", "Preencha o Titulo com no mínimo 2 caractere.")
               .HasMinLen(SlugPermissao, 2, "SlugPermissao", "Preencha o SlugPermissao com no mínimo 2 caractere.")              
           );
        }

        public void ValidAtualizar()
        {
            AddNotifications(
             new Contract()
               .Requires()
               .IsGreaterThan(Id, 0, "Id", "Id Inválido.")
           );
        }
    }
}
