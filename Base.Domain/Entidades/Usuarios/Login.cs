using Base.Domain.Entidades.Base;

namespace Base.Domain.Entidades.Usuarios
{
    public class Login: Entidade
    {
        public Login(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
