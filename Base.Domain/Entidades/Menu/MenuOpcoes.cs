using Base.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Domain.Entidades.Menu
{
    public class MenuOpcoes: Entidade
    {
        public MenuOpcoes()
        {
            MenuOpcoesBotoes = new HashSet<MenuOpcoesBotoes>();
            ModuloMenuOpcoes = new HashSet<ModuloMenuOpcoes>();
        }

        public MenuOpcoes(MenuOpcoes menuopcoes)
        {
            SetDTO(menuopcoes);
        }

        public void SetDTO(MenuOpcoes menuopcoes)
        {
            Id = menuopcoes.Id;
            IdMenu = menuopcoes.IdMenu;
            PathUrl = menuopcoes.PathUrl;
            SubMenu = menuopcoes.SubMenu;
            Titulo = menuopcoes.Titulo;
            Ativo = menuopcoes.Ativo;
            VisivelMenu = menuopcoes.VisivelMenu;
            SlugPermissao = menuopcoes.SlugPermissao;
        }

        public int Id { get; set; }
        public int? IdMenu { get; set; }
        public string PathUrl { get; set; }
        public int? SubMenu { get; set; }
        public string Titulo { get; set; }
        public bool Ativo { get; set; }
        public bool VisivelMenu { get; set; }
        public string SlugPermissao { get; set; }

        // public Menu IdMenuNavigation { get; set; }
        public ICollection<MenuOpcoesBotoes> MenuOpcoesBotoes { get; set; }
        public ICollection<ModuloMenuOpcoes> ModuloMenuOpcoes { get; set; }
    }
}
