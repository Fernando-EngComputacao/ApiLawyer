using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Lawyer.Assets.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly LawyerContext _context;
        private readonly IMapper _mapper;
        private readonly ProcessoValidator _validator;

        public ProcessoService(LawyerContext context, IMapper mapper, ProcessoValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ReadProcessoDTO> CreateProcessoAsync(CreateProcessoDTO dto)
        {
            ValidarRequestProcesso(dto);
            var processo = _mapper.Map<Processo>(dto);
            processo.Ativo = 1;
            _context.Processos.Add(processo);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadProcessoDTO>(processo);
        }

        public async Task<IEnumerable<ReadProcessoDTO>> GetAllProcessosAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadProcessoDTO>>(await _context.Processos.Skip(skip).Take(take).ToListAsync());
        }

        public async Task<IEnumerable<ReadProcessoDTO>> GetLogicDeletionProcessosAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadProcessoDTO>>(await _context.Processos.Where(processo => processo.Ativo == 1).Skip(skip).Take(take).ToListAsync());
        }

        public async Task<ReadProcessoDTO> GetProcessoByIdAsync(long id)
        {
            var processo = await _context.Processos.FirstOrDefaultAsync(o => o.Id == id);
            return processo != null ? _mapper.Map<ReadProcessoDTO>(processo) : null;
        }

        public async Task<Processo> UpdateProcessoAsync(int id, UpdateProcessoDTO dto)
        {
            ValidarRequestProcesso(_mapper.Map<CreateProcessoDTO>(dto));
            var processo = await _context.Processos.FirstOrDefaultAsync(o => o.Id == id);
            if (processo == null) return null;

            var result = _mapper.Map(dto, processo);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Processo> DeleteProcessoAsync(int id)
        {
            var processo = await _context.Processos.FirstOrDefaultAsync(o => o.Id == id);
            if (processo == null) return null;

            _context.Remove(processo);
            await _context.SaveChangesAsync();
            return processo;
        }

        public async Task<Processo> LogicalDeleteProcessoAsync(int id)
        {
            var processo = await _context.Processos.FirstOrDefaultAsync(o => o.Id == id);
            if (processo == null) return null;

            processo.Ativo = 0;
            await _context.SaveChangesAsync();
            return processo;
        }

        private void ValidarRequestProcesso(CreateProcessoDTO dto)
        {
            var validationResult = _validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validationResult.Errors);
                throw new LawyerException(errors, HttpStatusCode.BadRequest);
            }
        }
    }
}
