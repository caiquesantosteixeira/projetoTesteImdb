﻿using Base.Domain.Repositorios.Usuario;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Base.Infra.Repositorios.Usuario
{
    public class UserIdentity : IUserIdentity
    {
        private readonly IHttpContextAccessor _accessor;
        public UserIdentity(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string Nome => _accessor.HttpContext.User.Identity.Name;
        public string Email => GetUserEmail();

        public bool Administrador => GetUserIsAdministrador();

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
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