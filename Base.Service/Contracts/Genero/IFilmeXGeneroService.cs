using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXGeneroService
    {
        Task<ICommandResult> Persistir(FilmeXGeneroDTO command, ELogin acoes);
    }

}
