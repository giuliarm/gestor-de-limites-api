using GestorDeLimites.Application.Models.Requests;
using GestorDeLimites.Application.Models.Responses;
using GestorDeLimites.Domain.Dtos;

namespace GestorDeLimites.Application.Interfaces
{
    public interface ILimiteClienteService
    {
        Task<bool> CadastrarLimiteAsync(CadastrarLimiteRequest request);
        Task<LimiteClienteDto> BuscarLimiteAsync(BuscarLimiteRequest request);
        Task<bool> AlterarLimiteAsync(AlterarLimiteRequest request);
        Task<bool> RemoverLimiteAsync(RemoverLimiteRequest request);
        Task<VerificarTransacaoPixResponse> VerificarTransacaoPixAsync(VerificarTransacaoPixRequest request);
    }
}
