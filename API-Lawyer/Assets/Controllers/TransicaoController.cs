using API_Lawyer.Assets.Models.Transicao.dto;
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
    public class TransicaoController : ControllerBase
    {
        private readonly TransicaoService _transicaoService;

        public TransicaoController(TransicaoService transicaoService)
        {
            _transicaoService = transicaoService;
        }

        /// <summary> Cadastra uma transicao no banco de dados </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTransicao([FromBody] CreateTransicaoDTO dto)
        {
            var createdTransicao = await _transicaoService.CreateTransicaoAsync(dto);
            return CreatedAtAction(nameof(GetTransicaoById), new { id = createdTransicao.Id }, createdTransicao);
        }

        /// <summary> Busca todas as transições existentes no banco de dados com exceção as deletadas logicamente </summary>
        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionTransicaoAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _transicaoService.GetLogicDeletionTransicaoAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca todas as transições existentes no banco de dados incluso as deletadas logicamente </summary>
        [HttpGet("/Transicao/All")]
        public async Task<IActionResult> GetAllTransicao([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _transicaoService.GetAllTransicaoAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca uma transição pelo ID </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransicaoById(long id)
        {
            var transicao = await _transicaoService.GetTransicaoByIdAsync(id);
            return transicao != null ? Ok(transicao) : NotFound();
        }

        /// <summary> Altera uma transição pelo ID </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTransicao(int id, [FromBody] UpdateTransicaoDTO dto)
        {
            var result = await _transicaoService.UpdateTransicaoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta permanentemente uma transição pelo ID </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransicao(int id)
        {
            var result = await _transicaoService.DeleteTransicaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta logicamente uma transição pelo ID </summary>
        [HttpDelete("/Transicao/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteTransicao(int id)
        {
            var result = await _transicaoService.LogicalDeleteTransicaoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
