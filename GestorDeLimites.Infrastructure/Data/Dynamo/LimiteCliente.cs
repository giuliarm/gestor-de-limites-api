using Amazon.DynamoDBv2.DataModel;

namespace GestorDeLimites.Infrastructure.Data.Dynamo
{
    [DynamoDBTable("LimiteCliente")]
    public class LimiteCliente
    {
        [DynamoDBHashKey("Documento")]
        public string Documento { get; set; }

        [DynamoDBProperty("NumeroAgencia")]
        public string NumeroAgencia { get; set; }

        [DynamoDBProperty("NumeroConta")]
        public string NumeroConta { get; set; }

        [DynamoDBProperty("LimitePIX")]
        public decimal LimitePIX { get; set; }
    }
}
