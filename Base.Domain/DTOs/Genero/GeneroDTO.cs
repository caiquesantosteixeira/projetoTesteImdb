using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs
{
    public class GeneroDTO 
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
     
    }
}
