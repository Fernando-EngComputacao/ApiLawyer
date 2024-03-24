using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Security.autorizacao;
using API_Lawyer.Assets.Services;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequireAuthentication]
    [Authorize(Policy = "standard")]
    public class ProcessoController : ControllerBase
    {
        private readonly ProcessoService _processoService;

        public ProcessoController(ProcessoService processoService)
        {
            _processoService = processoService;
        }

        /// <summary> Cadastra um processo no banco de dados </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProcesso([FromBody] CreateProcessoDTO dto)
        {
            var createdProcesso = await _processoService.CreateProcessoAsync(dto);
            return CreatedAtAction(nameof(GetProcessoById), new { id = createdProcesso.Id }, createdProcesso);
        }

        /// <summary> Busca todos os processos existentes no banco de dados com exceção os deletados logicamente </summary>
        [HttpGet]
        public async Task<IActionResult> GetLoficDeletionProcessosAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _processoService.GetLogicDeletionProcessosAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca todos os processos existentes no banco de dados incluso os deletados logicamente </summary>
        [HttpGet("/Processo/All")]
        public async Task<IActionResult> GetAllProcessos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _processoService.GetAllProcessosAsync(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca um processo pelo ID </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessoById(long id)
        {
            var processo = await _processoService.GetProcessoByIdAsync(id);
            return processo != null ? Ok(processo) : NotFound();
        }

        /// <summary> Busca um processo pelo número do processo </summary>
        [HttpGet("/Processo/Add/{numeroProcesso}")]
        public async Task<IActionResult> GetProcessoByNumeroProcesso(string numeroProcesso)
        {
            var processo = await _processoService.GetProcessoByNumeroProcessoAsync(numeroProcesso);
            return processo != null ? Ok(processo) : NotFound();
        }

        /// <summary> Altera os dados de um processo pelo ID </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProcesso(int id, [FromBody] UpdateProcessoDTO dto)
        {
            var result = await _processoService.UpdateProcessoAsync(id, dto);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta um processo pelo ID de forma permanente </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcesso(int id)
        {
            var result = await _processoService.DeleteProcessoAsync(id);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary> Deleta um processo pelo ID de forma lógica </summary>
        [HttpDelete("/Processo/Logic/{id}")]
        public async Task<IActionResult> LogicalDeleteProcesso(int id)
        {
            var result = await _processoService.LogicalDeleteProcessoAsync(id);
            return result == null ? NotFound() : NoContent();
        }
    }
}
