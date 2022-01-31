using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IDiretorService
    {
        Task<Retorno> GetAll();
        Task<Retorno> Get(int id);
        Task<Retorno> Cadastrar(DiretorInsertDTO command);
        Task<Retorno> Atualizar(DiretorUpdateDTO command);
        Task<Retorno> Excluir(DiretorDTO command);

    }

}
