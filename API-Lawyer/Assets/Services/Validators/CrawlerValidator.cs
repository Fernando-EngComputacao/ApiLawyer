using API_Lawyer.Assets.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace API_Lawyer.Assets.Services.Validators
{
    public class CrawlerValidator : AbstractValidator<string>
    {
        private readonly LawyerContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrawlerValidator(LawyerContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(numeroProcesso => numeroProcesso)
                .NotEmpty().WithMessage("O número do processo não pode estar vazio.")
                .Matches(@"^\d{7}-\d{2}\.\d{4}\.\d{1,2}\.\d{2}\.\d{4}$")
                .WithMessage("O número do processo deve estar no formato correto: AAAAAAA-BB.CCCC.D.EE.FFFF");
                //.When(x => _httpContextAccessor.HttpContext.Request.Method == HttpMethod.Get.Method);

            RuleFor(numeroProcesso => numeroProcesso)
                .MustAsync(NumeroProcessoUnico)
                .WithMessage("Número do processo já cadastrado.")
                .When(x => _httpContextAccessor.HttpContext.Request.Method == HttpMethod.Post.Method);
        }

        private async Task<bool> NumeroProcessoUnico(string numeroProcesso, CancellationToken cancellationToken)
        {
            return await _context.Processos.AllAsync(p => p.NumeroProcesso != numeroProcesso);
        }
    }
}
