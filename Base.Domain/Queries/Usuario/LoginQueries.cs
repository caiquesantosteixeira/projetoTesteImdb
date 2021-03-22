using Base.Domain.Entidades.Usuarios;
using System;
using System.Linq.Expressions;

namespace Base.Domain.Queries.Usuario
{
    public class LoginQueries
    {        
       
        public static Expression<Func<Login, bool>> GetById(string user)
        {
            return x => x.Nome == user;
        }
    }
}
