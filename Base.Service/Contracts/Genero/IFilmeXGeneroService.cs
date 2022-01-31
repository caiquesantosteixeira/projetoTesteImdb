using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXGeneroService
    {
        Task<Retorno> Cadastrar(FilmeXGeneroInsertDTO command);
        Task<Retorno> Atualizar(FilmeXGeneroUpdateDTO command);
        Task<Retorno> Excluir(FilmeXGeneroDTO command);
        Task<Retorno> GetAll();
    }
}
