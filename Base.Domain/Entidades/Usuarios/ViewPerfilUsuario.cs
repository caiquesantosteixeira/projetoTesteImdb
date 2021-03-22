using Base.Domain.Entidades.Base;

namespace Base.Domain.Entidades.Usuarios
{
    public class ViewPerfilUsuario: Entidade
    {
        public int Id { get; set; }
        public int IdPerfil { get; set; }
        public string Menu { get; set; }
        public string Permissao { get; set; }
    }
}
