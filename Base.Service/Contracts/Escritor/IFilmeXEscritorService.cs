using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeXEscritorService
    {
        Task<Retorno> Cadastrar(FilmeXEscritorInsertDTO command);
        Task<Retorno> Atualizar(FilmeXEscritorUpdateDTO command);
        Task<Retorno> Excluir(FilmeXEscritorDTO command);
        Task<Retorno> GetAll();
    }
}
