using Flunt.Notifications;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Commands.Menu;
using Base.Domain.Commands.Menu.Enums;
using Base.Domain.Handler.Interface;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using System.Threading.Tasks;

namespace Base.Domain.Handler.Menu
{
    public class MenuHandler : Notifiable,
        IHandler<MenuCommand, EMenu>
    {
        private readonly IMenu _repository;
        public MenuHandler(IMenu repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(MenuCommand command, EMenu acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case EMenu.CADASTRAR:
                    retorno = await Cadastrar(command);
                    break;
                case EMenu.ATUALIZAR:
                    retorno = await Atualizar(command);
                    break;
            }

            return retorno;
        }

        private async Task<Retorno> Cadastrar(MenuCommand command)
        {
            var menu = new Base.Domain.Entidades.Menu.Menu();
            menu.Id = command.Id;
            menu.Menu1 = command.Menu1;
            menu.Icone = command.Icone;
            menu.Ordem = command.Ordem;

            var ret = await _repository.Cadastrar(menu);
            return new Retorno(true,"Cadastrado com sucesso", ret);
        }

        private async Task<Retorno> Atualizar(MenuCommand command)
        {
            command.ValidateAtualizar();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var menu = await _repository.GetById(command.Id);
            if(menu == null)
                return new Retorno(false, "Menu não cadastrado.", "Menu não cadastrado.");            
           
            menu.Menu1 = command.Menu1;
            menu.Icone = command.Icone;
            menu.Ordem = command.Ordem;

            var ret = await _repository.Atualizar(menu);
            return new Retorno (true, "Atualizado com sucesso", ret);
        }
    }
}
