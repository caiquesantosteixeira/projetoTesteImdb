using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXGeneroDTO
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdGenero { get; set; }
    }
}
