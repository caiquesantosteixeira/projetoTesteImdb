using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Base.Domain.Entidades
{
    public partial class Ator
    {
        public Ator()
        {
            FilmeXator = new HashSet<FilmeXator>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<FilmeXator> FilmeXator { get; set; }
    }
}
