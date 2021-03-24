using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class Escritor
    {
        public Escritor()
        {
            FilmeXescritor = new HashSet<FilmeXescritor>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<FilmeXescritor> FilmeXescritor { get; set; }
    }
}
