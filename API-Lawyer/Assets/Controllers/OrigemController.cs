using API_Lawyer.Assets.Model.Origem.dto;
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
    public class OrigemController : ControllerBase
    {
        private readonly OrigemService _origemService;

        public OrigemController(OrigemService origemService)
        {
            _origemService = origemService;
        }

        /// <summary> Cadastra uma origem no banco de dados </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrigem([FromBody] CreateOrigemDTO dto)
        {
            var createdOrigem = await _origemService.CreateOrigemAsync(dto);
            return CreatedAtAction(nameof(GetOrigemById), new { id = createdOrigem.Id }, createdOrigem);
        }

        /// <summary> Busca todas as origens existentes no banco de dados com exceção as deletadas logicamente </summary>
        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionOrigensAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _origemService.GetLogicDeletionOrigensAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca todas as origens existentes no banco de dados incluso as deletadas logicamente </summary>
        [HttpGet("/Ordem/All")]
        public async Task<IActionResult> GetAllOrigens([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _origemService.GetAllOrigensAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca uma origem pelo ID </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrigemById(long id)
        {
            var origem = await _origemService.GetOrigemByIdAsync(id);
            return origem != null ? Ok(origem) : NotFound();
        }

        /// <summary> Altera uma origem pelo ID </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrigem(int id, [FromBody] UpdateOrigemDTO dto)
        {
            var result = await _origemService.UpdateOrigemAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta permanentemente uma origem pelo ID </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrigem(int id)
        {
            var result = await _origemService.DeleteOrigemAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta logicamente uma origem pelo ID </summary>
        [HttpDelete("/Origem/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteOrigem(int id)
        {
            var result = await _origemService.LogicalDeleteOrigemAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
