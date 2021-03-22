using Base.Domain.Entidades.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Domain.Entidades.Menu
{
    public class ModuloMenuOpcoes: Entidade
    {
        public ModuloMenuOpcoes()
        {
            PerfilUsuario = new HashSet<PerfilUsuario>();
        }

        public ModuloMenuOpcoes(ModuloMenuOpcoes obj)
        {
            SetDTO(obj);
        }

        public void SetDTO(ModuloMenuOpcoes obj)
        {
            Id = obj.Id;
            IdModulo = obj.IdModulo;
            IdMenuOpcoes = obj.IdMenuOpcoes;
        }

        public int Id { get; set; }
        public int IdModulo { get; set; }
        public int IdMenuOpcoes { get; set; }

        [NotMapped]
        public string Modulo { get; set; }
        [NotMapped]
        public string MenuOpcoes { get; set; }
        [NotMapped]
        public bool Ativo { get; set; }

        public MenuOpcoes IdMenuOpcoesNavigation { get; set; }
        public Modulo IdModuloNavigation { get; set; }
        public ICollection<PerfilUsuario> PerfilUsuario { get; set; }
    }
}
