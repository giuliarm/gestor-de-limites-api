namespace GestorDeLimites.Application.Models.Requests
{
    public class AlterarLimiteRequest
    {
        public string Documento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal NovoLimite { get; set; }
    }
}
