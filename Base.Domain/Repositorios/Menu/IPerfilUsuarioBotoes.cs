using Base.Domain.Entidades.Menu;
using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IPerfilUsuarioBotoes
    {
        Task<IEnumerable<PerfilUsuarioBotoes>> GetAll();
        Task<PerfilUsuarioBotoes> Cadastrar(PerfilUsuarioBotoes perfi);
        Task<bool> Cadastrar(List<PerfilUsuarioBotoes> perfis);
        Task<PerfilUsuarioBotoes> GetPermissaoUsuario(int idPermissao, int idPerfilUsuario);
        Task<PerfilUsuarioBotoes> GetPermissaoById(int idPermissao);
        Task<IEnumerable<PerfilUsuarioBotoes>> GetAllPermissaoPerfil(int idPerfil);
        Task<Retorno> Excluir(int id);
    }
}
