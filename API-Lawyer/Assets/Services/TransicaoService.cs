using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Lawyer.Assets.Services
{
    public class TransicaoService : ITransicaoService
    {
        private readonly LawyerContext _context;
        private readonly IMapper _mapper;
        private readonly TransicaoValidator _validator;
        private readonly ILogger _logger;

        public TransicaoService(LawyerContext context, IMapper mapper, ILogger logger, TransicaoValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ReadTransicaoDTO> CreateTransicaoAsync(CreateTransicaoDTO dto)
        {
            ValidarRequestTransicao(dto);
            var transicao = _mapper.Map<Transicao>(dto);
            transicao.Ativo = 1;
            _context.Transicoes.Add(transicao);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadTransicaoDTO>(transicao);
        }

        public async Task<IEnumerable<ReadTransicaoDTO>> GetAllTransicoesAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadTransicaoDTO>>(await _context.Transicoes.Skip(skip).Take(take).ToListAsync());
        }

        public async Task<IEnumerable<ReadTransicaoDTO>> GetLogicDeletionTransicoesAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadTransicaoDTO>>(await _context.Transicoes.Where(transicao => transicao.Ativo == 1).Skip(skip).Take(take).ToListAsync());
        }

        public async Task<ReadTransicaoDTO> GetTransicaoByIdAsync(long id)
        {
            var transicao = await _context.Transicoes.FirstOrDefaultAsync(o => o.Id == id);
            return transicao != null ? _mapper.Map<ReadTransicaoDTO>(transicao) : null;
        }

        public async Task<Transicao> UpdateTransicaoAsync(int id, UpdateTransicaoDTO dto)
        {
            ValidarRequestTransicao(_mapper.Map<CreateTransicaoDTO>(dto));
            var transicao = await _context.Transicoes.FirstOrDefaultAsync(o => o.Id == id);
            if (transicao == null) return null;

            var result = _mapper.Map(dto, transicao);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Transicao> DeleteTransicaoAsync(int id)
        {
            var transicao = await _context.Transicoes.FirstOrDefaultAsync(o => o.Id == id);
            if (transicao == null) return null;

            _context.Remove(transicao);
            await _context.SaveChangesAsync();
            return transicao;
        }

        public async Task<Transicao> LogicalDeleteTransicaoAsync(int id)
        {
            var transicao = await _context.Transicoes.FirstOrDefaultAsync(o => o.Id == id);
            if (transicao == null) return null;

            transicao.Ativo = 0;
            await _context.SaveChangesAsync();
            return transicao;
        }

        public Task<IEnumerable<ReadTransicaoDTO>> GetAllTransicaoAsync(int skip = 0, int take = 10)
        {
            throw new NotImplementedException();
        }

        private void ValidarRequestTransicao(CreateTransicaoDTO dto)
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

