using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrigemController : ControllerBase
    {
        private LawyerContext _context;
        private IMapper _mapper;

        public OrigemController(LawyerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult registerOrigem([FromBody] CreateOrigemDTO dto)
        {
            Origem origem = _mapper.Map<Origem>(dto);
            origem.Ativo = 1;
            _context.Origens.Add(origem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(recoverOrigemtById), new { id = origem.Id }, origem);
        }

        /// <summary> Busca a lista inteira de Origens</summary>
        [HttpGet]
        public IEnumerable<ReadOrigemDTO> recoverAllOrigens([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadOrigemDTO>>(_context.Origens.Where(origem => origem.Ativo == 1).Skip(skip).Take(take).ToList());

        }

        [HttpGet("/Origem/All")]
        public IEnumerable<ReadOrigemDTO> recoverOrigem([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadOrigemDTO>>(_context.Origens.Skip(skip).Take(take).ToList());

        }


        [HttpGet("{id}")]
        public IActionResult recoverOrigemtById(long id)
        {
            var result = _context.Origens.FirstOrDefault(origem => origem.Id == id);
            return (result != null ? Ok(_mapper.Map<ReadOrigemDTO>(result)) : NotFound());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateOrigem(int id, [FromBody] UpdateOrigemDTO dto)
        {
            Origem origem = _context.Origens.FirstOrDefault(origem => origem.Id == id);
            if (origem == null) return NotFound();
            _mapper.Map(dto, origem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult updatePatchOrigem(int id, JsonPatchDocument<UpdateOrigemDTO> patch)
        {
            Origem origem = _context.Origens.FirstOrDefault(origem => origem.Id == id);
            if (origem == null) return NotFound();

            var toUpdateOrigem = _mapper.Map<UpdateOrigemDTO>(origem);
            patch.ApplyTo(toUpdateOrigem, ModelState);
            if (!TryValidateModel(toUpdateOrigem)) return ValidationProblem(ModelState);

            _mapper.Map(toUpdateOrigem, origem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult removeOrigem(int id)
        {
            Origem origem = _context.Origens.FirstOrDefault(origem => origem.Id == id);
            if (origem == null) return NotFound();
            _context.Remove(origem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("/Origem/Logic/{id}")]
        public IActionResult removeLogicalOrigem(int id)
        {
            Origem origem = _context.Origens.FirstOrDefault(origem => origem.Id == id);
            if (origem == null) return NotFound();
            origem.Ativo = 0;
            _context.SaveChanges();
            return NoContent();
        }


    }
}
