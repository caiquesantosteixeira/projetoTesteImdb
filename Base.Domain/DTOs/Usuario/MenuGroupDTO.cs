using System.Collections.Generic;

namespace Base.Domain.DTOs.Usuario
{
    public class MenuGroupDTO
    {
        public string Title { get; set; }
        public string Path { get { return "/"; } }
        public string Type { get { return "sub"; } }
        public int? Ordem { get; set; }
        public string Icontype { get; set; } = "pe-7s-plus";
        public List<OpcoesDTO> Children { get; set; }
    }
}
