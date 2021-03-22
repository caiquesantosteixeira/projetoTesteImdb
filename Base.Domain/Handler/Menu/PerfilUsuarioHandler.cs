using Flunt.Notifications;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Commands.Usuario;
using Base.Domain.Commands.Usuario.Enums;
using Base.Domain.Handler.Interface;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Domain.Handler.Menu
{
    public class PerfilUsuarioHandler : Notifiable,
        IHandler<PerfilUsuarioCommand, EPerfilUsuario>
    {
        private readonly IPerfilUsuario _repository;
        
        public PerfilUsuarioHandler(IPerfilUsuario repository)
        {
            _repository = repository;
        }


        public async Task<ICommandResult> Handle(PerfilUsuarioCommand command, EPerfilUsuario acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await Adicionar(command);            
        }

        private async Task<Retorno> Adicionar(PerfilUsuarioCommand command)
        {
            var existPerfil = await _repository.Consultar(command.IdPerfil, command.IdModuloMenuOpc);
            if (existPerfil != null)
            {
                return new Retorno (false, "Opção já existe", "Opção já existe.");
            }

            var perfilusuario = await _repository.Cadastrar(command.GetPerfilUsuario());
            return new Retorno (true, "Perfil cadastrado com sucesso.", perfilusuario);
        }
    }

}
