using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Processo.dto;
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
        private LawyerContext _context;
        private IMapper _mapper;

        public ProcessoController(LawyerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult registerProcesso([FromBody] CreateProcessoDTO dto)
        {
            Processo processo = _mapper.Map<Processo>(dto);
            processo.Ativo = 1;
            _context.Processos.Add(processo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(recoverProcessotById), new { id = processo.Id }, processo);
        }

        /// <summary> Busca a lista inteira de Processos</summary>
        [HttpGet]
        public IEnumerable<ReadProcessoDTO> recoverAllProcessos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadProcessoDTO>>(_context.Processos.Where(processo => processo.Ativo == 1).Skip(skip).Take(take).ToList());

        }

        [HttpGet("/Processo/All")]
        public IEnumerable<ReadProcessoDTO> recoverProcesso([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadProcessoDTO>>(_context.Processos.Skip(skip).Take(take).ToList());

        }


        [HttpGet("{id}")]
        public IActionResult recoverProcessotById(long id)
        {
            var result = _context.Processos.FirstOrDefault(processo => processo.Id == id);
            return (result != null ? Ok(_mapper.Map<ReadProcessoDTO>(result)) : NotFound());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProcesso(int id, [FromBody] UpdateProcessoDTO dto)
        {
            Processo processo = _context.Processos.FirstOrDefault(processo => processo.Id == id);
            if (processo == null) return NotFound();
            _mapper.Map(dto, processo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult updatePatchProcesso(int id, JsonPatchDocument<UpdateProcessoDTO> patch)
        {
            Processo processo = _context.Processos.FirstOrDefault(processo => processo.Id == id);
            if (processo == null) return NotFound();

            var toUpdateProcesso = _mapper.Map<UpdateProcessoDTO>(processo);
            patch.ApplyTo(toUpdateProcesso, ModelState);
            if (!TryValidateModel(toUpdateProcesso)) return ValidationProblem(ModelState);

            _mapper.Map(toUpdateProcesso, processo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult removeProcesso(int id)
        {
            Processo processo = _context.Processos.FirstOrDefault(processo => processo.Id == id);
            if (processo == null) return NotFound();
            _context.Remove(processo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("/Processo/Logic/{id}")]
        public IActionResult removeLogicalProcesso(int id)
        {
            Processo processo = _context.Processos.FirstOrDefault(processo => processo.Id == id);
            if (processo == null) return NotFound();
            processo.Ativo = 0;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
