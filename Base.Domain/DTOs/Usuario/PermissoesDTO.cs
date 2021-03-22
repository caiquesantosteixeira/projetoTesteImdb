namespace Base.Domain.DTOs.Usuario
{
    public class PermissoesDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string TypeCampo { get; set; }

        public int IdPerfilUsuario { get; set; }
    }
}
