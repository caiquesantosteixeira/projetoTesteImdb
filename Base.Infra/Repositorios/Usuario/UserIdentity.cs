using Base.Rpepository.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Base.Repository.Repositorios.Usuario
{
    public class UserIdentity : IUserIdentity
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly testeimdbContext _ctx;
        public UserIdentity(IHttpContextAccessor accessor, testeimdbContext context)
        {
            _accessor = accessor;
            _ctx = context;
        }
        public string Nome => _accessor.HttpContext.User.Identity.Name;
        public string Email => GetUserEmail();

        public bool Administrador => GetUserIsAdministrador();


        public bool ValidarUsuario()
        {
            var username = _accessor.HttpContext.User.GetUserId();
            var usuario = _ctx.Usuarios.FirstOrDefault(x => x.UserName.Equals(username));

            if (usuario != null)
            {
                return usuario.Administrador ?? false;
            }
            else
            {
                return false;
            }
        }

        public string GetUserId()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserId() : string.Empty;
        }

        public bool GetUserIsAdministrador()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.IsAdministrador() : false;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static bool IsAdministrador(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(c => c.Type.ToUpper().Equals("ADMINISTRADOR"));
            if (claim == null)
                return false;

            return bool.Parse(claim.Value);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
