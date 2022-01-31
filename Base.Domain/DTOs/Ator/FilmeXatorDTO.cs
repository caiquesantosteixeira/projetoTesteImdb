using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class FilmeXatorDTO 
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdAtor { get; set; }
    }
}
