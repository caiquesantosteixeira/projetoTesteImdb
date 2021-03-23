using Base.Domain.DTOs.Usuario;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using Base.Domain.ValueObject.Basicos;
using System.Threading.Tasks;

namespace Base.Service.Contracts.Usuario
{
    public  interface IUsuarioService
    {

        Task<ICommandResult> Persistir(UsuarioDTO command, ELogin acoes);
        Task<ICommandResult> AlterarSenha(AlterarSenhaDTO command, ELogin acoes);
        Task<Retorno> GetAll();
        Task<Retorno> Logar(LoginDTO login);
        Task<Retorno> Get(string id);
    }
}
