using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilmeXDiretor
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(FilmeXdiretor filmeXDiretor);
        Task<Retorno> Atualizar(FilmeXdiretor filmeXFilmeXDiretor);
        Task<Retorno> Excluir(int id);
    }

}
