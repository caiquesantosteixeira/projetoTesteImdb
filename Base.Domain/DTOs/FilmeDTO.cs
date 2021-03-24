using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Domain.DTOs
{
    public class FilmeDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public decimal Tempo { get; set; }
        public int Ano { get; set; }
        public string Foto { get; set; }
    }
}
