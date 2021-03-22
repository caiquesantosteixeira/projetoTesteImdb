using Base.Domain.Entidades.Base;
using System.Collections.Generic;

namespace Base.Domain.Entidades.Menu
{
    public class Menu: Entidade
    {
        public Menu(Menu menu)
        {
            SetDTO(menu);
        }

        public void SetDTO(Menu menu)
        {
            Id = menu.Id;
            Menu1 = menu.Menu1;
            Icone = menu.Icone;
            Ordem = menu.Ordem;
        }

        public Menu()
        {
            //MenuOpcoes = new HashSet<MenuOpcoes>();
        }


        public int Id { get; set; }
        public string Menu1 { get; set; }
        public string Icone { get; set; }
        public int? Ordem { get; set; }

        // public ICollection<MenuOpcoes> MenuOpcoes { get; set; }
    }
}
