using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXNotaService
    {
        Task<ICommandResult> Persistir(FilmeXNotaDTO command, ELogin acoes);
    }
}
