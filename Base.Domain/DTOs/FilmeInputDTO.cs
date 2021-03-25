using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Base.Domain.DTOs
{
    public class FilmeInputDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Resumo { get; set; }
        [Required]
        public decimal Tempo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public string Foto { get; set; }
    }
}
