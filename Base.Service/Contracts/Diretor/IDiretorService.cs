using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IDiretorService
    {
        Task<ICommandResult> Persistir(DiretorDTO command, ELogin acoes);
        Task<Retorno> GetAll();
        Task<Retorno> Get(string id);
    }

}
