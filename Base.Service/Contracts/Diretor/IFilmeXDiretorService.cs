using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXDiretorService
    {
        Task<Retorno> Cadastrar(FilmeXDiretorInsertDTO command);
        Task<Retorno> Atualizar(FilmeXDiretorUpdateDTO command);
        Task<Retorno> Excluir(FilmeXDiretorDTO command);
        Task<Retorno> GetAll();
    }

}
