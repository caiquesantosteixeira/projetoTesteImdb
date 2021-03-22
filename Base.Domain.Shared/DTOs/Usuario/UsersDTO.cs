namespace Base.Domain.Shared.DTOs.Usuario
{
    public class UsersDTO
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NomeDoUsuario { get; set; }
        public int IdPerfil { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public bool Administrador { get; set; }
    }
}
