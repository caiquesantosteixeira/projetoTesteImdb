using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilmeXEscritor
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(FilmeXescritor filmeXescritor);
        Task<Retorno> Atualizar(FilmeXescritor filmeXescritor);
        Task<Retorno> Excluir(int id);
    }

}
