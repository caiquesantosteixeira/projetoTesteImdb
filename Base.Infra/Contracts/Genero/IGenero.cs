using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IGenero:IBaseRepository<Genero>
    {
        Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null);
    }
}
