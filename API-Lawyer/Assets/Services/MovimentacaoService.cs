using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Lawyer.Assets.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
            private readonly LawyerContext _context;
            private readonly IMapper _mapper;
            private readonly MovimentacaoValidator _validator;

            public MovimentacaoService(LawyerContext context, IMapper mapper, MovimentacaoValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

        public async Task<ReadMovimentacaoDTO> CreateMovimentacaoAsync(CreateMovimentacaoDTO dto)
        {
            ValidarRequestMovimentacao(dto);
            var movimentacao = _mapper.Map<Movimentacao>(dto);
            movimentacao.Ativo = 1;
            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadMovimentacaoDTO>(movimentacao);
        }

        public async Task<IEnumerable<ReadMovimentacaoDTO>> GetAllMovimentacoesAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadMovimentacaoDTO>>(await _context.Movimentacoes.Skip(skip).Take(take).ToListAsync());
        }

        public async Task<IEnumerable<ReadMovimentacaoDTO>> GetLogicDeletionMovimentacoesAsync(int skip = 0, int take = 10)
        {
            return _mapper.Map<List<ReadMovimentacaoDTO>>(await _context.Movimentacoes.Where(movimentacao => movimentacao.Ativo == 1).Skip(skip).Take(take).ToListAsync());
        }

        public async Task<ReadMovimentacaoDTO> GetMovimentacaoByIdAsync(long id)
        {
            var movimentacao = await _context.Movimentacoes.FirstOrDefaultAsync(o => o.Id == id);
            return movimentacao != null ? _mapper.Map<ReadMovimentacaoDTO>(movimentacao) : null;
        }

        public async Task<int?> GetIdAsync()
        {
            var movimentacao = await _context.Movimentacoes.OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return movimentacao != null ? movimentacao.Id : null;
        }

        public async Task<Movimentacao> UpdateMovimentacaoAsync(int id, UpdateMovimentacaoDTO dto)
        {
            ValidarRequestMovimentacao(_mapper.Map<CreateMovimentacaoDTO>(dto));
            var movimentacao = await _context.Movimentacoes.FirstOrDefaultAsync(o => o.Id == id);
            if (movimentacao == null) return null;

            var result = _mapper.Map(dto, movimentacao);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Movimentacao> DeleteMovimentacaoAsync(int id)
        {
            var movimentacao = await _context.Movimentacoes.FirstOrDefaultAsync(o => o.Id == id);
            if (movimentacao == null) return null;

            _context.Remove(movimentacao);
            await _context.SaveChangesAsync();
            return movimentacao;
        }

        public async Task<Movimentacao> LogicalDeleteMovimentacaoAsync(int id)
        {
            var movimentacao = await _context.Movimentacoes.FirstOrDefaultAsync(o => o.Id == id);
            if (movimentacao == null) return null;

            movimentacao.Ativo = 0;
            await _context.SaveChangesAsync();
            return movimentacao;
        }

        private void ValidarRequestMovimentacao(CreateMovimentacaoDTO dto)
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
