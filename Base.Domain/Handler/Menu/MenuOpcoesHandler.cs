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
    public class MenuOpcoesHandler : Notifiable,
        IHandler<MenuOpcoesCommand, EMenuOpcoes>
    {
        private readonly IMenuOpcoes _repository;
        public MenuOpcoesHandler(IMenuOpcoes menuOpcoes)
        {
            _repository = menuOpcoes;
        }
        public async Task<ICommandResult> Handle(MenuOpcoesCommand command, EMenuOpcoes acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case EMenuOpcoes.CADASTRAR:
                    retorno = await Cadastrar(command);
                    break;
                case EMenuOpcoes.ATUALIZAR:
                    retorno = await Atualizar(command);
                    break;                            
            }

            return retorno;
        }        

        private async Task<Retorno> Cadastrar(MenuOpcoesCommand command)
        {

            MenuOpcoes menu = new MenuOpcoes();
            menu.IdMenu = command.IdMenu;
            menu.PathUrl = command.PathUrl;
            menu.SubMenu = command.SubMenu;
            menu.Titulo = command.Titulo;
            menu.Ativo = command.Ativo;
            menu.VisivelMenu = command.VisivelMenu;
            menu.SlugPermissao = command.SlugPermissao;
            var ret = await _repository.Cadastrar(menu);

            return new Retorno (true, "Menu Cadastrado com Successo.", ret);
        }

        private async Task<Retorno> Atualizar(MenuOpcoesCommand command)
        {
            command.ValidAtualizar();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var menu = await _repository.MenuOpcoesById(command.Id);
            if(menu == null)
            {
                return new Retorno (false, "Menu não cadastrado.", "Menu não cadastrado.");
            }

            menu.IdMenu          = command.IdMenu;
            menu.PathUrl         = command.PathUrl;
            menu.SubMenu         = command.SubMenu;
            menu.Titulo          = command.Titulo;
            menu.Ativo           = command.Ativo;
            menu.VisivelMenu     = command.VisivelMenu;
            menu.SlugPermissao   = command.SlugPermissao;
            var ret = await _repository.Atualizar(menu);

            return new Retorno (true, "Menu Atualizado com Successo.", ret);
        }
    }
}
