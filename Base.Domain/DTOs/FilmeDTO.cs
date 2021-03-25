﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Domain.DTOs
{
    public class FilmeDTO
    {
        public FilmeDTO() {
            Atores = new List<AtorDTO>();
            Diretores = new List<DiretorDTO>();
            Generos = new List<GeneroDTO>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public decimal Tempo { get; set; }
        public int Ano { get; set; }
        public string Foto { get; set; }

        public int MediaNota { get; set; }

        public List<AtorDTO> Atores { get; set; }

        public List<DiretorDTO> Diretores { get; set; }

        public List<GeneroDTO> Generos { get; set; }
    }
}
