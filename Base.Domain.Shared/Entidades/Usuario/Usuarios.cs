using Base.Domain.Shared.DTOs.Usuario;
using Microsoft.AspNetCore.Identity;


namespace Base.Domain.Shared.Entidades.Usuario
{
    public class Usuarios : IdentityUser
    {
        public int IdPerfil { get; set; }
        public bool? Ativo { get; set; }
        public string Nome { get; set; }
        public bool? Administrador { get; set; }

        public UsersDTO GetDTO()
        {
            var user = new UsersDTO();
            user.Id = Id;
            user.Login = UserName;
            user.Senha = PasswordHash;
            user.NomeDoUsuario = Nome;
            user.IdPerfil = IdPerfil;
            user.Ativo = (Ativo ?? false);
            user.Email = Email;
            user.Administrador = (Administrador??false);
            return user;
        }
    }
}
