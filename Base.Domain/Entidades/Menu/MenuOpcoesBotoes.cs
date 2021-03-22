using Base.Domain.Entidades.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Domain.Entidades.Menu
{
    public class MenuOpcoesBotoes: Entidade
    {
        public MenuOpcoesBotoes()
        {

        }

        public MenuOpcoesBotoes(MenuOpcoesBotoes menuopbotoes)
        {
            SetDTO(menuopbotoes);
        }

        public void SetDTO(MenuOpcoesBotoes menuopbotoes)
        {
            IdPermissoes = menuopbotoes.IdPermissoes;
            IdMenuOpcoes = menuopbotoes.IdMenuOpcoes;
        }

        public int Id { get; set; }
        public int IdPermissoes { get; set; }
        public int IdMenuOpcoes { get; set; }
        [NotMapped]
        public string DescPermissao { get; set; }

        public MenuOpcoes IdMenuOpcoesNavigation { get; set; }
        public Permissoes IdPermissoesNavigation { get; set; }
    }
}
