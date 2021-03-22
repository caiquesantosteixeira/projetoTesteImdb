using System.Collections.Generic;

namespace Base.Domain.DTOs.Usuario
{
    public class OpcoesDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public int? SubMenu { get; set; }
        public string MenuDescricao { get; set; }
        public int IdPerfil { get; set; }
        public int IdPerfilUsu { get; set; }
        public int? Ordem { get; set; }
        public string Ab { get { return "pe-7s-angle-right"; } }
        public bool Ativo { get; set; }
        public bool VisivelMenu { get; set; }
        public List<PermissoesDTO> Permissoes { get; set; }
    }
}
