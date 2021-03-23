using Base.Domain.DTOs.Usuario;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Retornos;
using Base.Domain.ValueObject.Basicos;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios.Usuario
{
    public  interface IUsuario
    {
        Task<Retorno> Logar(LoginDTO login);
        Task<UsuarioLogadoDTO> GerarJwtAsync(UsuarioLogadoDTO parcial);      
        Task<Retorno> GetAll();
        Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null);
        Task<Retorno> GetById(string id);
        Task<Retorno> Cadastrar(UsuarioDTO usuario);
        Task<Retorno> Atualizar(UsuarioDTO usuario);
        Task<Retorno> AlterarSenha(AlterarSenhaDTO command);
        Task<Retorno> AlterarSenha(Email email, string novaSenha);
        Task<Retorno> Excluir(string id);
    }
}
