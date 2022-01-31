using Base.Domain.DTOs;
using Base.Domain.DTOs.Ator;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXAtorService
    {
        Task<Retorno> Cadastrar(FilmeXatorInsertDTO command);

        Task<Retorno> Atualizar(FilmeXatorUpdateDTO command);

        Task<Retorno> Excluir(FilmeXatorDTO command);

        Task<Retorno> GetAll();
    }

}
