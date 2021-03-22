using Flunt.Notifications;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Commands.Usuario;
using Base.Domain.Commands.Usuario.Enums;
using Base.Domain.Handler.Interface;
using Base.Domain.Repositorios.Usuario;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Domain.Handler.Usuario
{
    public class UsuarioHandler : Notifiable,
        IHandler<UsuarioCommand, ELogin>,
        IHandler<AlterarSenhaCommand, ELogin>
    {
        private readonly IUsuario _repository;
        public UsuarioHandler(IUsuario repository)
        {
            _repository = repository;
        }
        
        public async Task<ICommandResult> Handle(UsuarioCommand command, ELogin acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case ELogin.CADASTRAR:
                    retorno = await Cadastrar(command);
                    break;
                case ELogin.ATUALIZAR:
                    retorno = await Atualizar(command);
                    break;
                case ELogin.EXCLUIR:
                     retorno = await Excluir(command);
                    break;                
            }

            return retorno;
        }

        public async Task<ICommandResult> Handle(AlterarSenhaCommand command, ELogin acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.AlterarSenha(command);
        }

        private async Task<Retorno> Cadastrar(UsuarioCommand command)
        {
            command.ValidateCadastrar();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Cadastrar(command);
        }

        private async Task<Retorno> Atualizar(UsuarioCommand command)
        {
            command.ValidAtualizarExcluir();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Atualizar(command);

        }

        private async Task<Retorno> Excluir(UsuarioCommand command)
        {
            command.ValidAtualizarExcluir();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Excluir(command.Id);
        }
    }
}
