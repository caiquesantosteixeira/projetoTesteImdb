using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilme:IBaseRepository<Filme>
    {
        Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null);
    }

}
