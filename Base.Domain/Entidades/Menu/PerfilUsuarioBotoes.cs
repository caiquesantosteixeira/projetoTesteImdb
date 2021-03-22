using Base.Domain.Entidades.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Domain.Entidades.Menu
{
    public class PerfilUsuarioBotoes: Entidade
    {
        public int Id { get; set; }
        public int IdPermissoes { get; set; }
        public int IdPerfilUsuario { get; set; }

        [NotMapped]
        public string Nome { get; set; }

        public PerfilUsuario IdPerfilUsuarioNavigation { get; set; }
        public Permissoes IdPermissoesNavigation { get; set; }

        public PerfilUsuarioBotoes()
        {

        }

        public PerfilUsuarioBotoes(PerfilUsuarioBotoes obj)
        {
            SetDTO(obj);
        }

        public void SetDTO(PerfilUsuarioBotoes obj)
        {
            Id = obj.Id;
            IdPermissoes = obj.IdPermissoes;
            IdPerfilUsuario = obj.IdPerfilUsuario;
        }

    }
}
