using Base.Domain.Entidades.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Domain.Entidades.Menu
{
    public class PerfilUsuario: Entidade
    {
        public PerfilUsuario()
        {
            PerfilUsuarioBotoes = new HashSet<PerfilUsuarioBotoes>();
        }       

        public int Id { get; set; }
        public int IdModuloMenuOpc { get; set; }
        public int IdPerfil { get; set; }

        [NotMapped]
        public string Titulo { get; set; }

        [NotMapped]
        public int IdMenuOpcoes { get; set; }

        [NotMapped]
        public bool VisivelMenu { get; set; }


        public ModuloMenuOpcoes IdModuloMenuOpcNavigation { get; set; }
        public Perfil IdPerfilNavigation { get; set; }
        public ICollection<PerfilUsuarioBotoes> PerfilUsuarioBotoes { get; set; }
    }
}
