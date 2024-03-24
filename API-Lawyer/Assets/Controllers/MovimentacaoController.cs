using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Assets.Security.autorizacao;
using API_Lawyer.Assets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequireAuthentication]
    [Authorize(Policy = "standard")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly MovimentacaoService _movimentacaoService;

        public MovimentacaoController(MovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }
        /// <summary> Cadastra uma movimentação no banco de dados </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMovimentacao([FromBody] CreateMovimentacaoDTO dto)
        {
            var createdMovimentacao = await _movimentacaoService.CreateMovimentacaoAsync(dto);
            return CreatedAtAction(nameof(GetMovimentacaoById), new { id = createdMovimentacao.Id }, createdMovimentacao);
        }

        /// <summary> Busca todas as movimentações existentes no banco de dados com exceção as deletadas logicamente </summary>
        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionMovimentacoesAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _movimentacaoService.GetLogicDeletionMovimentacoesAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca todas as movimentações existentes no banco de dados incluso as deletadas logicamente </summary>
        [HttpGet("/Movimentacao/All")]
        public async Task<IActionResult> GetAllMovimentacoes([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _movimentacaoService.GetAllMovimentacoesAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca uma movimentação pelo ID </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimentacaoById(long id)
        {
            var movimentacao = await _movimentacaoService.GetMovimentacaoByIdAsync(id);
            return movimentacao != null ? Ok(movimentacao) : NotFound();
        }

        /// <summary> Altera uma movimentação pelo ID </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMovimentacao(int id, [FromBody] UpdateMovimentacaoDTO dto)
        {
            var result = await _movimentacaoService.UpdateMovimentacaoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta permanentemente uma movimentação pelo ID </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacao(int id)
        {
            var result = await _movimentacaoService.DeleteMovimentacaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta logicamente uma movimentação pelo ID </summary>
        [HttpDelete("/Movimentacao/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteMovimentacao(int id)
        {
            var result = await _movimentacaoService.LogicalDeleteMovimentacaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
