﻿using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXDiretorDTO
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdDiretor { get; set; }
    }
}
