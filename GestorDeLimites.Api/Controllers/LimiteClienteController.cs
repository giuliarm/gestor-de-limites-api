using GestorDeLimites.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GestorDeLimites.Application.Constants;
using GestorDeLimites.Application.Models.Requests;

namespace GestorDeLimites.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LimiteClienteController : ControllerBase
    {
        private readonly ILimiteClienteService _limiteClienteService;

        public LimiteClienteController(ILimiteClienteService limiteClienteService)
        {
            _limiteClienteService = limiteClienteService;
        }

        /// <summary>
        /// Cadastra um novo limite para o cliente.
        /// </summary>
        /// <param name="request">Dados do limite a ser cadastrado.</param>
        /// <returns>Indica se a operação foi bem-sucedida.</returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarLimite([FromBody] CadastrarLimiteRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _limiteClienteService.CadastrarLimiteAsync(request);
            return result ? Ok(Retorno.LimiteCadastradoSucesso) : BadRequest(Retorno.LimiteCadastroFalha);
        }

        /// <summary>
        /// Busca o limite para uma conta já cadastrada.
        /// </summary>
        /// <param name="request">Dados da conta para buscar o limite.</param>
        /// <returns>Dados do limite cadastrado.</returns>
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarLimite([FromQuery] BuscarLimiteRequest request)
        {
            var limite = await _limiteClienteService.BuscarLimiteAsync(request);
            return limite != null ? Ok(limite) : NotFound(Retorno.LimiteNaoEncontrado);
        }

        /// <summary>
        /// Altera o limite para uma conta já cadastrada.
        /// </summary>
        /// <param name="request">Dados do limite a ser alterado.</param>
        /// <returns>Indica se a operação foi bem-sucedida.</returns>
        [HttpPut("alterar")]
        public async Task<IActionResult> AlterarLimite([FromBody] AlterarLimiteRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _limiteClienteService.AlterarLimiteAsync(request);
            return result ? Ok(Retorno.LimiteAlteradoSucesso) : BadRequest(Retorno.LimiteAlteracaoFalha);
        }

        /// <summary>
        /// Remove um registro de limite do banco de dados.
        /// </summary>
        /// <param name="request">Dados do limite a ser removido.</param>
        /// <returns>Indica se a operação foi bem-sucedida.</returns>
        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverLimite([FromQuery] RemoverLimiteRequest request)
        {
            var result = await _limiteClienteService.RemoverLimiteAsync(request);
            return result ? Ok(Retorno.LimiteRemovidoSucesso) : BadRequest(Retorno.LimiteRemocaoFalha);
        }

        /// <summary>
        /// Avalia se uma transação PIX pode ser realizada com base no limite disponível.
        /// </summary>
        /// <param name="request">Dados da transação a ser avaliada.</param>
        /// <returns>Indica se a transação pode ser realizada.</returns>
        [HttpPost("avaliar-transacao")]
        public async Task<IActionResult> AvaliarTransacao([FromBody] VerificarTransacaoPixRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _limiteClienteService.VerificarTransacaoPixAsync(request);

            if(resultado == null)
            {
                return NotFound(Retorno.LimiteNaoEncontrado);
            }

            if (resultado.StatusTransacao == Retorno.TransacaoAprovada)
                return Ok(resultado);
            else
                return BadRequest(new { message = Retorno.TransacaoNaoAprovada, resultado });
        }
    }
}

