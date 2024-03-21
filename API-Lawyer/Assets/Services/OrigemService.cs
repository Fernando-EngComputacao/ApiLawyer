using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Services
{
    public class OrigemService : IOrigemService
    {

        private readonly LawyerContext _context;
        private readonly IMapper _mapper;

        public OrigemService(LawyerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadOrigemDTO> CreateOrigemAsync(CreateOrigemDTO dto)
        {
            var origem = _mapper.Map<Origem>(dto);
            origem.Ativo = 1;
            _context.Origens.Add(origem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadOrigemDTO>(origem);
        }

        public async Task<IEnumerable<ReadOrigemDTO>> GetAllOrigensAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadOrigemDTO>>(await _context.Origens.Skip(skip).Take(take).ToListAsync());
        }

        public async Task<IEnumerable<ReadOrigemDTO>> GetLogicDeletionOrigensAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadOrigemDTO>>(await _context.Origens.Where(origem => origem.Ativo == 1).Skip(skip).Take(take).ToListAsync());
        }

        public async Task<ReadOrigemDTO> GetOrigemByIdAsync(long id)
        {
            var origem = await _context.Origens.FirstOrDefaultAsync(o => o.Id == id);
            return origem != null ? _mapper.Map<ReadOrigemDTO>(origem) : null;
        }

        public async Task<Origem> UpdateOrigemAsync(int id, UpdateOrigemDTO dto)
        {
            var origem = await _context.Origens.FirstOrDefaultAsync(o => o.Id == id);
            if (origem == null) return null;

            var result = _mapper.Map(dto, origem);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Origem> DeleteOrigemAsync(int id)
        {
            var origem = await _context.Origens.FirstOrDefaultAsync(o => o.Id == id);
            if (origem == null) return null;

            _context.Remove(origem);
            await _context.SaveChangesAsync();
            return origem;
        }

        public async Task<Origem> LogicalDeleteOrigemAsync(int id)
        {
            var origem = await _context.Origens.FirstOrDefaultAsync(o => o.Id == id);
            if (origem == null) return null;

            origem.Ativo = 0;
            await _context.SaveChangesAsync();
            return origem;
        }
    }
}
