using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class Genero
    {
        public Genero()
        {
            FilmeXgenero = new HashSet<FilmeXgenero>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<FilmeXgenero> FilmeXgenero { get; set; }
    }
}
