using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class FilmeXescritor
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdEscritor { get; set; }

        public virtual Escritor IdEscritorNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
