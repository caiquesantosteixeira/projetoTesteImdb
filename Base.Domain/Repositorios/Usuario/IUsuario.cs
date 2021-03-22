using Base.Domain.Commands.Usuario;
using Base.Domain.DTOs.Usuario;
using Base.Domain.Retornos;
using Base.Domain.ValueObject.Basicos;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Usuario
{
    public  interface IUsuario
    {
        Task<Retorno> Logar(LoginDTO login);
        Task<UsuarioLogadoDTO> GerarJwtAsync(UsuarioLogadoDTO parcial);      
        Task<Retorno> GetAll();
        Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null);
        Task<Retorno> GetById(string id);
        Task<Retorno> Cadastrar(UsuarioCommand usuario);
        Task<Retorno> Atualizar(UsuarioCommand usuario);
        Task<Retorno> AlterarSenha(AlterarSenhaCommand command);
        Task<Retorno> AlterarSenha(Email email, string novaSenha);
        Task<Retorno> Excluir(string id);
    }
}
