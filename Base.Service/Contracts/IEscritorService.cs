using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IEscritorService
    {
        Task<ICommandResult> Persistir(EscritorDTO command, ELogin acoes);
        Task<Retorno> GetAll();
        Task<Retorno> Get(string id);
    }

}
