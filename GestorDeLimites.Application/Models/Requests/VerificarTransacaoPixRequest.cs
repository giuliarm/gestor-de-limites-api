namespace GestorDeLimites.Application.Models.Requests
{
    public class VerificarTransacaoPixRequest
    {
        public string Documento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
