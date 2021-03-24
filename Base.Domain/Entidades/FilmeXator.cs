using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class FilmeXator
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdAtor { get; set; }

        public virtual Ator IdAtorNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
