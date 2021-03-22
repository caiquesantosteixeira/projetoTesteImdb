using Base.Domain.Entidades.Menu;
using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IMenuOpcoes
    {
        Task<IEnumerable<object>> OpcoesEmpresa();
        Task<IEnumerable<MenuOpcoes>> GetAll();
        Task<IEnumerable<object>> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null);
        Task<MenuOpcoes> Atualizar(MenuOpcoes menuopcoes);
        Task<MenuOpcoes> Cadastrar(MenuOpcoes menuopcoes);
        Task<Retorno> Excluir(int id);
        Task<MenuOpcoes> MenuOpcoesById(int id);
    }
}
