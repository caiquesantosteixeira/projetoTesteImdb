namespace Base.Domain.ValueObject.Config
{
    public class AppSettings
    {
        public string Secret { get { return "8c8c38055fe8a3014c0e5b1845291952"; } }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
