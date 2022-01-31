using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXNotaDTO
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public string IdUsuario { get; set; }
        public int Nota { get; set; }
        
    }
}
