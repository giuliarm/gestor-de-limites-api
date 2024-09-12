using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using GestorDeLimites.Domain.Dtos;
using GestorDeLimites.Domain.Repositories;
using GestorDeLimites.Infrastructure.Data.Dynamo;

namespace GestorDeLimites.Infrastructure.Data
{
    public class LimiteClienteRepository : ILimiteClienteRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly IMapper _mapper;

        public LimiteClienteRepository(IDynamoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CadastrarLimiteClienteAsync(LimiteClienteDto request)
        {
            var cliente = _mapper.Map<LimiteCliente>(request);

            await _context.SaveAsync(cliente);
        }

        public async Task<LimiteClienteDto> ObterClienteAsync(string documento, string agencia, string conta)
        {
            var table = _context.GetTargetTable<LimiteCliente>();

            var queryConfig = new QueryOperationConfig
            {
                IndexName = "Documento-index",
                Limit = 1,
                KeyExpression = new Expression
                {
                    ExpressionStatement = "#kn0 = :kv0",
                    ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#kn0", "Documento" },
                },
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":kv0", documento },
                }
                },
                FilterExpression = new Expression
                {
                    ExpressionStatement = "#n0 = :v0 AND #kn1 = :kv1",
                    ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#kn1", "NumeroAgencia" },
                    { "#n0", "NumeroConta" }
                },
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {   { ":kv1", agencia },
                    { ":v0", conta }
                }
                },

            };

            var search = table.Query(queryConfig);

            var results = await search.GetNextSetAsync();

            var item = results.FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            var limiteClienteDto = new LimiteClienteDto
            {
                Documento = item["Documento"],
                NumeroAgencia = item["NumeroAgencia"],
                NumeroConta = item["NumeroConta"],
                LimitePIX = Convert.ToDecimal(item["LimitePIX"])
            };

            return limiteClienteDto;
        }

        public async Task RemoverClienteAsync(LimiteClienteDto request)
        {
            var cliente = _mapper.Map<LimiteCliente>(request);

            await _context.DeleteAsync(cliente);
        }
    }
}
