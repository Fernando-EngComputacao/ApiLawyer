using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Assets.Services;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrigemController : ControllerBase
    {
        private readonly OrigemService _origemService;

        public OrigemController(OrigemService origemService)
        {
            _origemService = origemService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrigem([FromBody] CreateOrigemDTO dto)
        {
            var createdOrigem = await _origemService.CreateOrigemAsync(dto);
            return CreatedAtAction(nameof(GetOrigemById), new { id = createdOrigem.Id }, createdOrigem);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionOrigensAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _origemService.GetLoficDeletionOrigensAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("/Ordem/All")]
        public async Task<IActionResult> GetAllOrigens([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _origemService.GetAllOrigensAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrigemById(long id)
        {
            var origem = await _origemService.GetOrigemByIdAsync(id);
            return origem != null ? Ok(origem) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrigem(int id, [FromBody] UpdateOrigemDTO dto)
        {
            var result = await _origemService.UpdateOrigemAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrigem(int id)
        {
            var result = await _origemService.DeleteOrigemAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("/Origem/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteOrigem(int id)
        {
            var result = await _origemService.LogicalDeleteOrigemAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
