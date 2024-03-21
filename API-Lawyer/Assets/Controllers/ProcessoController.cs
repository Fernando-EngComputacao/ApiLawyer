using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Services;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessoController : ControllerBase
    {
        private readonly ProcessoService _processoService;

        public ProcessoController(ProcessoService processoService)
        {
            _processoService = processoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProcesso([FromBody] CreateProcessoDTO dto)
        {
            var createdProcesso = await _processoService.CreateProcessoAsync(dto);
            return CreatedAtAction(nameof(GetProcessoById), new { id = createdProcesso.Id }, createdProcesso);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionProcessosAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _processoService.GetLogicDeletionProcessosAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("/Processo/All")]
        public async Task<IActionResult> GetAllProcessos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _processoService.GetAllProcessosAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessoById(long id)
        {
            var processo = await _processoService.GetProcessoByIdAsync(id);
            return processo != null ? Ok(processo) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProcesso(int id, [FromBody] UpdateProcessoDTO dto)
        {
            var result = await _processoService.UpdateProcessoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcesso(int id)
        {
            var result = await _processoService.DeleteProcessoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("/Processo/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteProcesso(int id)
        {
            var result = await _processoService.LogicalDeleteProcessoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
