using GestorDeLimites.Domain.Dtos;

namespace GestorDeLimites.Domain.Repositories
{
    public interface ILimiteClienteRepository
    {
        Task CadastrarLimiteClienteAsync(LimiteClienteDto request);
        Task<LimiteClienteDto> ObterClienteAsync(string documento, string agencia, string conta);
        Task RemoverClienteAsync(LimiteClienteDto request);
    }
}
