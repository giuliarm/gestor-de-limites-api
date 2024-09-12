namespace GestorDeLimites.Domain.Dtos
{
    public class LimiteClienteDto
    {
        public string Documento { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public decimal LimitePIX { get; set; }
    }
}
