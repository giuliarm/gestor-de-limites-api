using AutoMapper;
using GestorDeLimites.Application.Constants;
using GestorDeLimites.Application.Interfaces;
using GestorDeLimites.Application.Models.Requests;
using GestorDeLimites.Application.Models.Responses;
using GestorDeLimites.Domain.Dtos;
using GestorDeLimites.Domain.Repositories;

namespace GestorDeLimites.Application.Services
{
    public class LimiteClienteService : ILimiteClienteService
    {
        private readonly ILimiteClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public LimiteClienteService(ILimiteClienteRepository limiteClienteRepository, IMapper mapper) {
            _clienteRepository = limiteClienteRepository;
            _mapper = mapper;
        }

        public async Task<bool> CadastrarLimiteAsync(CadastrarLimiteRequest request)
        {
            var cliente = _mapper.Map<LimiteClienteDto>(request);

            await _clienteRepository.CadastrarLimiteClienteAsync(cliente);
            return true;
        }
        public async Task<LimiteClienteDto> BuscarLimiteAsync(BuscarLimiteRequest request)
        {
            var cliente = await _clienteRepository.ObterClienteAsync(request.Documento, request.Agencia, request.Conta);
            if (cliente == null)
                return null;

            return cliente;
        }

        public async Task<bool> AlterarLimiteAsync(AlterarLimiteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var cliente = _mapper.Map<LimiteClienteDto>(request);

            await _clienteRepository.CadastrarLimiteClienteAsync(cliente);
            return true;
        }

        public async Task<bool> RemoverLimiteAsync(RemoverLimiteRequest request)
        {
            var cliente = _mapper.Map<LimiteClienteDto>(request);

            await _clienteRepository.RemoverClienteAsync(cliente);
            return true;
        }

        public async Task<VerificarTransacaoPixResponse> VerificarTransacaoPixAsync(VerificarTransacaoPixRequest request)
        {
            var buscarLimiteRequest = _mapper.Map<BuscarLimiteRequest>(request);

            var clienteExistente = await BuscarLimiteAsync(buscarLimiteRequest);
            if (clienteExistente == null)
                return null;

            bool statusTransacao = clienteExistente.LimitePIX >= request.ValorTransacao;
            if (statusTransacao)
            {
                clienteExistente.LimitePIX -= request.ValorTransacao;
                await _clienteRepository.CadastrarLimiteClienteAsync(clienteExistente);
            }

            return new VerificarTransacaoPixResponse
            {
                StatusTransacao = statusTransacao ? Retorno.TransacaoAprovada : Retorno.LimiteInsuficiente,
                ValorLimiteAtual = clienteExistente.LimitePIX
            };
        }
    }
}
