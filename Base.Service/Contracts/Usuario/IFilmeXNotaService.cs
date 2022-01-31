using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXNotaService
    {
        Task<Retorno> Cadastrar(FilmeXNotaDTO command);
        Task<Retorno> Atualizar(FilmeXNotaDTO command);
        Task<Retorno> Excluir(FilmeXNotaDTO command);
        Task<Retorno> GetAll();
    }
}
