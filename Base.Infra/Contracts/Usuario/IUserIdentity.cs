using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Base.Repository.Repositorios.Usuario
{
    public interface IUserIdentity
    {
        string Nome { get; }
        string Email { get; }
        string GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
        bool ValidarUsuario();
    }
}
