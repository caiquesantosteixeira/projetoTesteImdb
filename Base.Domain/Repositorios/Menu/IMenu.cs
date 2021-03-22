using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IMenu
    {
        Task<IEnumerable<Base.Domain.Entidades.Menu.Menu>> GetAll();
        Task<Base.Domain.Entidades.Menu.Menu> GetById(int id);
        Task<Base.Domain.Entidades.Menu.Menu> Cadastrar(Base.Domain.Entidades.Menu.Menu menu);
        Task<Base.Domain.Entidades.Menu.Menu> Atualizar(Base.Domain.Entidades.Menu.Menu menu);
        Task<Retorno> Excluir(int id);
    }
}
