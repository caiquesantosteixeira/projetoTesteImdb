using Flunt.Notifications;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Commands.Menu;
using Base.Domain.Commands.Menu.Enums;
using Base.Domain.Entidades.Menu;
using Base.Domain.Handler.Interface;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Domain.Handler.Menu
{
    public class PerfilUsuarioBotoesHandler : Notifiable,
        IHandler<PerfilUsuarioBotoesCommand, EPerfilUsuarioBotoes>
    {
        private readonly IPerfilUsuarioBotoes _repository;
        public PerfilUsuarioBotoesHandler(IPerfilUsuarioBotoes perfilUsuarioBotoes)
        {
            _repository = perfilUsuarioBotoes;
        }

        public async Task<ICommandResult> Handle(PerfilUsuarioBotoesCommand command, EPerfilUsuarioBotoes acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case EPerfilUsuarioBotoes.CADASTRAR:
                    retorno = await Adicionar(command);
                    break;               
            }

            return retorno;
        }

        private async Task<Retorno> Adicionar(PerfilUsuarioBotoesCommand command)
        {
            var perfBt = await _repository.GetPermissaoUsuario(command.IdPermissoes,command.IdPerfilUsuario);
            if(perfBt != null)
                return new Retorno(false,"Não Autorizado!", "Permissão já cadastrada.");

            PerfilUsuarioBotoes obj = new PerfilUsuarioBotoes();
            obj.Id = command.Id;
            obj.IdPermissoes = command.IdPermissoes;
            obj.IdPerfilUsuario = command.IdPerfilUsuario;

            var ret = await _repository.Cadastrar(obj);

            return new Retorno (true, "PerfilUsuarioBotoes Cadastrado com Successo.", ret);
        }
    }
}
