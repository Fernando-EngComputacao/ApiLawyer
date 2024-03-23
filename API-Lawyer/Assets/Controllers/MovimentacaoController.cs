using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Assets.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly MovimentacaoService _movimentacaoService;

        public MovimentacaoController(MovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMovimentacao([FromBody] CreateMovimentacaoDTO dto)
        {
            var createdMovimentacao = await _movimentacaoService.CreateMovimentacaoAsync(dto);
            return CreatedAtAction(nameof(GetMovimentacaoById), new { id = createdMovimentacao.Id }, createdMovimentacao);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionMovimentacoesAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _movimentacaoService.GetLogicDeletionMovimentacoesAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("/Movimentacao/All")]
        public async Task<IActionResult> GetAllMovimentacoes([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _movimentacaoService.GetAllMovimentacoesAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimentacaoById(long id)
        {
            var movimentacao = await _movimentacaoService.GetMovimentacaoByIdAsync(id);
            return movimentacao != null ? Ok(movimentacao) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMovimentacao(int id, [FromBody] UpdateMovimentacaoDTO dto)
        {
            var result = await _movimentacaoService.UpdateMovimentacaoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacao(int id)
        {
            var result = await _movimentacaoService.DeleteMovimentacaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("/Movimentacao/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteMovimentacao(int id)
        {
            var result = await _movimentacaoService.LogicalDeleteMovimentacaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
