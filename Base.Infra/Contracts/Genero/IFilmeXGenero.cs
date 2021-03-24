using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilmeXGenero
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(FilmeXgenero fgenero);
        Task<Retorno> Atualizar(FilmeXgenero fgenero);
        Task<Retorno> Excluir(int id);
    }
}
