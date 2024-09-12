namespace GestorDeLimites.Application.Models.Requests
{
    public class RemoverLimiteRequest
    {
        public string Documento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }
}
