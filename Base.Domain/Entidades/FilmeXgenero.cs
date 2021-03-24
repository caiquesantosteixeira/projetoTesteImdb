using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class FilmeXgenero
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdGenero { get; set; }

        public virtual Filme IdFilmeNavigation { get; set; }
        public virtual Genero IdGeneroNavigation { get; set; }
    }
}
