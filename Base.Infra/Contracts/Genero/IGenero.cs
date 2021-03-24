using Base.Domain.Entidades;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IGenero
    {
        Task<Retorno> GetAll();
        Task<Retorno> Cadastrar(Genero filmeXgenero);
        Task<Retorno> Atualizar(Genero filmeXgenero);
        Task<Retorno> Excluir(int id);

        Task<Retorno> GetById(string id);
    }
}
