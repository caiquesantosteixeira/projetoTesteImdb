using Base.Domain.Entidades.Menu;
using Base.Domain.Retornos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Repositorios.Menu
{
    public interface IPerfil
    {
        Task<IEnumerable<Perfil>> GetAll();
        Task<Perfil> PerfilById(int id);
        Task<Perfil> Atualizar(Perfil perfilUsuario);
        Task<Perfil> Cadastrar(Perfil perfilUsuario);
        Task<Retorno> Excluir(int id);       
    }
}
