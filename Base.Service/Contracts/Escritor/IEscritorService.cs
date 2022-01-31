using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IEscritorService
    {
        Task<Retorno> Cadastrar(EscritorDTO command);

        Task<Retorno> Atualizar(EscritorDTO command);

        Task<Retorno> Excluir(EscritorDTO command);

        Task<Retorno> GetAll();

        Task<Retorno> Get(int id);
    }

}
