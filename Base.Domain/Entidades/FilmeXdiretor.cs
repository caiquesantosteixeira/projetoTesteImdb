using System;
using System.Collections.Generic;

namespace Base.Domain.Entidades
{
    public partial class FilmeXdiretor
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdDiretor { get; set; }

        public virtual Diretor IdDiretorNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
