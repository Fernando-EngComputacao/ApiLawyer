using API_Lawyer.Assets.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Services.Validators
{
    public class CrawlerValidator : AbstractValidator<string>
    {
        private readonly LawyerContext _context;
        public CrawlerValidator(LawyerContext context)
        {
            _context = context;

            RuleFor(numeroProcesso => numeroProcesso)
                .NotEmpty().WithMessage("O número do processo não pode estar vazio.")
                .Matches(@"^\d{7}-\d{2}\.\d{4}\.\d{1,2}\.\d{2}\.\d{4}$")
                .WithMessage("O número do processo deve estar no formato correto: AAAAAAA-BB.CCCC.D.EE.FFFF");
                //.MustAsync(NumeroProcessoUnico)
                //.WithMessage("Número do processo já cadastrado.");

        }

        private async Task<bool> NumeroProcessoUnico(string numeroProcesso, CancellationToken cancellationToken)
        {
            // Verifica se o número do processo já existe no banco de dados
            return await _context.Processos.AllAsync(p => p.NumeroProcesso != numeroProcesso);
        }
    }
}
