using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class Filme
    {
        public Filme()
        {
            FilmeXator = new HashSet<FilmeXator>();
            FilmeXdiretor = new HashSet<FilmeXdiretor>();
            FilmeXescritor = new HashSet<FilmeXescritor>();
            FilmeXgenero = new HashSet<FilmeXgenero>();
            FilmeXnota = new HashSet<FilmeXnota>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public decimal Tempo { get; set; }
        public int Ano { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<FilmeXator> FilmeXator { get; set; }
        public virtual ICollection<FilmeXdiretor> FilmeXdiretor { get; set; }
        public virtual ICollection<FilmeXescritor> FilmeXescritor { get; set; }
        public virtual ICollection<FilmeXgenero> FilmeXgenero { get; set; }
        public virtual ICollection<FilmeXnota> FilmeXnota { get; set; }
    }
}
