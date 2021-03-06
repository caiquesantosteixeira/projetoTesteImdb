using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IGeneroService
    {
        Task<ICommandResult> Persistir(GeneroDTO command, ELogin acoes);
        Task<Retorno> GetAll();
        Task<Retorno> Get(string id);
    }
}
