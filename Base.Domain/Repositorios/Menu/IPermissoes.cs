using Base.Domain.Entidades.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IPermissoes
    {
        Task<IEnumerable<Permissoes>> GetAll();
        Task<IEnumerable<Object>> GetPerMenuOpcoes(int idmenuopc);
    }
}
