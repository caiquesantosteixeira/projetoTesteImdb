using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class Diretor
    {
        public Diretor()
        {
            FilmeXdiretor = new HashSet<FilmeXdiretor>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<FilmeXdiretor> FilmeXdiretor { get; set; }
    }
}
