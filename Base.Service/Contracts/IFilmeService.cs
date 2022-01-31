using Base.Domain.DTOs;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Service.Contracts
{
    public interface IFilmeService
    {
        Task<Retorno> Cadastrar(FilmeInputDTO command);
        Task<Retorno> Atualizar(FilmeInputDTO command);
        Task<Retorno> Excluir(FilmeInputDTO command);

        Task<Retorno> GetAll();
        Task<Retorno> Get(int id);

        Task<Retorno> GetAllPaginado(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null);
    }

}
