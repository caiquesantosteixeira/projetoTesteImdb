using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeService
    {
        Task<Retorno> Cadastrar(FilmeInsertDTO command);
        Task<Retorno> Atualizar(FilmeUpdateDTO command);
        Task<Retorno> Excluir(FilmeDTO command);

        Task<Retorno> GetAll();
        Task<Retorno> Get(int id);

        Task<Retorno> GetAllPaginado(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null);
    }

}
