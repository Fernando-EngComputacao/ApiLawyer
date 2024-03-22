using FluentValidation;

namespace API_Lawyer.Assets.Services.Validators
{
    public class CrawlerValidator : AbstractValidator<string>
    {
        public CrawlerValidator()
        {
            RuleFor(numeroProcesso => numeroProcesso)
                .NotEmpty().WithMessage("O número do processo não pode estar vazio.")
                .Matches(@"^\d{7}-\d{2}\.\d{4}\.\d{1,2}\.\d{2}\.\d{4}$")
                .WithMessage("O número do processo deve estar no formato correto: AAAAAAA-BB.CCCC.D.EE.FFFF");
        }
    }
}
