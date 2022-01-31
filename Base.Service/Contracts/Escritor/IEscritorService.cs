using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IEscritorService
    {
        Task<Retorno> Cadastrar(EscritorInsertDTO command);

        Task<Retorno> Atualizar(EscritorUpdateDTO command);

        Task<Retorno> Excluir(EscritorDTO command);

        Task<Retorno> GetAll();

        Task<Retorno> Get(int id);
    }

}
