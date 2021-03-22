using Base.Domain.Entidades.Menu;
using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IMenuOpcoesBotoes
    {
        Task<IEnumerable<MenuOpcoesBotoes>> GetAll();
        Task<IEnumerable<MenuOpcoesBotoes>> GetPermissoesMenu(int idMenuOpcoes);
        Task<MenuOpcoesBotoes> GetById(int id);
        Task<MenuOpcoesBotoes> Get(int idPermissoes, int idMenuOpcoes);
        Task<MenuOpcoesBotoes> Cadastrar(MenuOpcoesBotoes menuOpcoesBotoes);
        Task<Retorno> Excluir(int id);
    }
}
