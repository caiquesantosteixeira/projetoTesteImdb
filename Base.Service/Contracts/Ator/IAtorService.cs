using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IAtorService
    {
        Task<Retorno> Cadastrar(AtorInsertDTO command);
        Task<Retorno> Atualizar(AtorUpdateDTO command);
        Task<Retorno> Excluir(AtorUpdateDTO command);
        Task<Retorno> GetAll();
        Task<Retorno> Get(int id);
    }

}
