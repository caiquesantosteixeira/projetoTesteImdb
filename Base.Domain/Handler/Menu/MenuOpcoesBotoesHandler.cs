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
    public class MenuOpcoesBotoesHandler : Notifiable,
        IHandler<MenuOpcoesBotoesCommand, EMenuOpcoesBotoes>
    {
        private readonly IMenuOpcoesBotoes _repository;
        public MenuOpcoesBotoesHandler(IMenuOpcoesBotoes repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(MenuOpcoesBotoesCommand command, EMenuOpcoesBotoes acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case EMenuOpcoesBotoes.CADASTRAR:
                    retorno = await Cadastrar(command);
                    break;                
            }

            return retorno;
        }

        private async Task<Retorno> Cadastrar(MenuOpcoesBotoesCommand command)
        {
            var consulta = await _repository.Get(command.IdPermissoes, command.IdMenuOpcoes);
            if (consulta != null)
                return new Retorno(false, "Não Autorizado", "Permissão já está cadastrada.");

            var menu = new MenuOpcoesBotoes();           
            menu.IdPermissoes = command.IdPermissoes;
            menu.IdMenuOpcoes = command.IdMenuOpcoes;           

            var ret = await _repository.Cadastrar(menu);
            return new Retorno (true, "Cadastrado com sucesso", ret);
        }
    }
}
