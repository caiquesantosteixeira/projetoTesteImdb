using Base.Domain.Entidades.Menu;
using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IPerfilUsuario
    {
        Task<object> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null, int id = 0);
        Task<object> PermissaoById(int idPerfil);
        Task<IEnumerable<PerfilUsuario>> perfilUsuarioById(int idPerfil);
        Task<PerfilUsuario> Cadastrar(PerfilUsuario perfilUsuario);
        Task<Retorno> Excluir(int idPerfil);
        Task<PerfilUsuario> Consultar(int IdPerfil, int IdModuloMenuOpcoes);
    }
}
