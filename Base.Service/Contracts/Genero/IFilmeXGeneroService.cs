using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXGeneroService
    {
        Task<Retorno> Cadastrar(FilmeXGeneroDTO command);
        Task<Retorno> Atualizar(FilmeXGeneroDTO command);
        Task<Retorno> Excluir(FilmeXGeneroDTO command);
        Task<Retorno> GetAll();
    }
}
