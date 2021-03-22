using Base.Domain.Entidades.Base;
using System.Collections.Generic;

namespace Base.Domain.Entidades.Menu
{
    public class Perfil: Entidade
    {
        public Perfil(Perfil perfil)
        {
            SetDTO(perfil);
        }

        public void SetDTO(Perfil perfil)
        {
            Id = perfil.Id;
            Nome = perfil.Nome;
        }

        public Perfil()
        {
            PerfilUsuario = new HashSet<PerfilUsuario>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<PerfilUsuario> PerfilUsuario { get; set; }
    }
}
