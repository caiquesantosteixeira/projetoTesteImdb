using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IGeneroService
    {
        Task<Retorno> Cadastrar(GeneroDTO command);
        Task<Retorno> Atualizar(GeneroDTO command);
        Task<Retorno> Excluir(GeneroDTO command);
        Task<Retorno> GetAll();
        Task<Retorno> Get(int id);
    }
}
