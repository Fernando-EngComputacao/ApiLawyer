using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private LawyerContext _context;
        private IMapper _mapper;

        public MovimentacaoController(LawyerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult registerMovimentacao([FromBody] CreateMovimentacaoDTO dto)
        {
            Movimentacao movimentacao = _mapper.Map<Movimentacao>(dto);
            movimentacao.Ativo = 1;
            _context.Movimentacoes.Add(movimentacao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(recoverMovimentacaoById), new { id = movimentacao.Id }, movimentacao);
        }

        /// <summary> Busca a lista inteira de Origens</summary>
        [HttpGet]
        public IEnumerable<ReadMovimentacaoDTO> recoverAllMovimentacao([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadMovimentacaoDTO>>(_context.Movimentacoes.Where(movimentacao => movimentacao.Ativo == 1).Skip(skip).Take(take).ToList());

        }

        [HttpGet("/Movimentacao/All")]
        public IEnumerable<ReadMovimentacaoDTO> recoverMovimentacao([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadMovimentacaoDTO>>(_context.Movimentacoes.Skip(skip).Take(take).ToList());

        }


        [HttpGet("{id}")]
        public IActionResult recoverMovimentacaoById(long id)
        {
            var result = _context.Origens.FirstOrDefault(movimentacao => movimentacao.Id == id);
            return (result != null ? Ok(_mapper.Map<ReadMovimentacaoDTO>(result)) : NotFound());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateMovimentacao(int id, [FromBody] UpdateMovimentacaoDTO dto)
        {
            Movimentacao movimentacao = _context.Movimentacoes.FirstOrDefault(movimentacao => movimentacao.Id == id);
            if (movimentacao == null) return NotFound();
            _mapper.Map(dto, movimentacao);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatchMovimentacao(int id, JsonPatchDocument<UpdateMovimentacaoDTO> patch)
        {
            Movimentacao movimentacao = _context.Movimentacoes.FirstOrDefault(movimentacao => movimentacao.Id == id);
            if (movimentacao == null) return NotFound();

            var toUpdateMovimentacao = _mapper.Map<UpdateMovimentacaoDTO>(movimentacao);
            patch.ApplyTo(toUpdateMovimentacao, ModelState);
            if (!TryValidateModel(toUpdateMovimentacao)) return ValidationProblem(ModelState);

            _mapper.Map(toUpdateMovimentacao, movimentacao);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveMovimentacao(int id)
        {
            Movimentacao movimentacao = _context.Movimentacoes.FirstOrDefault(movimentacao => movimentacao.Id == id);
            if (movimentacao == null) return NotFound();
            _context.Remove(movimentacao);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("/Movimentacao/Logic/{id}")]
        public IActionResult RemoveLogicalMovimentacao(int id)
        {
            Movimentacao movimentacao = _context.Movimentacoes.FirstOrDefault(movimentacao => movimentacao.Id == id);
            if (movimentacao == null) return NotFound();
            movimentacao.Ativo = 0;
            _context.SaveChanges();
            return NoContent();
        }


    }
}
