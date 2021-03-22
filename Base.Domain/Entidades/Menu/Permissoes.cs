using Base.Domain.Entidades.Base;
using System.Collections.Generic;

namespace Base.Domain.Entidades.Menu
{
    public class Permissoes: Entidade
    {
        public Permissoes()
        {
            MenuOpcoesBotoes = new HashSet<MenuOpcoesBotoes>();
            PerfilUsuarioBotoes = new HashSet<PerfilUsuarioBotoes>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string TypeCampo { get; set; }

        public ICollection<MenuOpcoesBotoes> MenuOpcoesBotoes { get; set; }
        public ICollection<PerfilUsuarioBotoes> PerfilUsuarioBotoes { get; set; }
    }
}
