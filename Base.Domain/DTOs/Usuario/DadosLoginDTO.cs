using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Base.Domain.DTOs.Usuario
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(3, ErrorMessage = "o campo {0} deve conter pelo menos 3 caracteres")]
        public string Senha { get; set; }
    }

    public class TokenUsuario
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Usuario { get; set; }

        public int IdPerfil { get; set; }

        public bool Administrador { get; set; }
        // public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioLogadoDTO
    {
        public string AccesToken { get; set; }

        public double ExpiresIn { get; set; }

        public TokenUsuario UserToken { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }

        public string Type { get; set; }
    }
}
