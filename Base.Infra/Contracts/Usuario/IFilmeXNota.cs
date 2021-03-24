using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IFilmeXNota
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(FilmeXnota filmeXnota);
        Task<Retorno> Atualizar(FilmeXnota filmeXnota);
        Task<Retorno> Excluir(int id);
    }
}
