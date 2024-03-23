using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Assets.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransicaoController : ControllerBase
    {
        private readonly TransicaoService _transicaoService;

        public TransicaoController(TransicaoService transicaoService)
        {
            _transicaoService = transicaoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTransicao([FromBody] CreateTransicaoDTO dto)
        {
            var createdTransicao = await _transicaoService.CreateTransicaoAsync(dto);
            return CreatedAtAction(nameof(GetTransicaoById), new { id = createdTransicao.Id }, createdTransicao);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionTransicaoAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _transicaoService.GetLogicDeletionTransicaoAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("/Transicao/All")]
        public async Task<IActionResult> GetAllTransicao([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _transicaoService.GetAllTransicaoAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransicaoById(long id)
        {
            var transicao = await _transicaoService.GetTransicaoByIdAsync(id);
            return transicao != null ? Ok(transicao) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTransicao(int id, [FromBody] UpdateTransicaoDTO dto)
        {
            var result = await _transicaoService.UpdateTransicaoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransicao(int id)
        {
            var result = await _transicaoService.DeleteTransicaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("/Transicao/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteTransicao(int id)
        {
            var result = await _transicaoService.LogicalDeleteTransicaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
