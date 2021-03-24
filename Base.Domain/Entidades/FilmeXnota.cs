using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class FilmeXnota
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public int IdFilme { get; set; }
        public int Nota { get; set; }

        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
