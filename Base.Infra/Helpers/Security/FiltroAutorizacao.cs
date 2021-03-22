using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Base.Infra.Helpers.Security
{
    public class FiltroAutorizacao
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                    LocalizarClaim(context).ToList().Any(c => c.Type.ToUpper() == claimName.ToUpper() && c.Value.ToUpper().Contains(claimValue.ToUpper()));
                   //context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

        public static List<Claim> LocalizarClaim(HttpContext context)
        {
            List<Claim> lisClaim = new List<Claim>();
            foreach (Claim item in context.User.Claims)
            {
                if (item.Type.ToUpper() == "ROLES")
                {
                    var aPerm = item.Value.Split("_");

                    var resp = lisClaim.FirstOrDefault(x => x.Type == aPerm[0]);

                    // Se não localizar, tem que adicionar uma vazia
                    if (resp == null)
                    {
                        lisClaim.Add(new Claim(aPerm[0], ""));
                        lisClaim.Add(new Claim(aPerm[0], aPerm[1]));
                    }
                    else
                    {
                        lisClaim.Add(new Claim(aPerm[0], aPerm[1]));
                    }
                }
            }
            return lisClaim;
        }
    }   

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claiName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claiName, claimValue) };
        }

    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!FiltroAutorizacao.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

        }
    }
}
