using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IAtor
    {
        Task<Retorno> GetAll();
        Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null);
        Task<Retorno> GetById(string id);
        Task<Retorno> Cadastrar(Ator ator);
        Task<Retorno> Atualizar(Ator ator);
        Task<Retorno> Excluir(int id);
    }

}
