using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilmeXAtor
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(FilmeXator filmeXator);
        Task<Retorno> Atualizar(FilmeXator filmeXFilmeXAtor);
        Task<Retorno> Excluir(int id);

    }

}
