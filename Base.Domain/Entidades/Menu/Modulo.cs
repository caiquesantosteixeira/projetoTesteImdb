using Base.Domain.Entidades.Base;
using System.Collections.Generic;

namespace Base.Domain.Entidades.Menu
{
    public class Modulo: Entidade
    {
        public Modulo(Modulo modulo)
        {
            Id = modulo.Id;
            Nome = modulo.Nome;
        }

        public Modulo()
        {
            ModuloMenuOpcoes = new HashSet<ModuloMenuOpcoes>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<ModuloMenuOpcoes> ModuloMenuOpcoes { get; set; }
    }
}
