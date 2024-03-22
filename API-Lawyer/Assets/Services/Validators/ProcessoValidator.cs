using API_Lawyer.Assets.Model.Processo.dto;
using FluentValidation;

namespace API_Lawyer.Assets.Services.Validators
{
    public class ProcessoValidator : AbstractValidator<CreateProcessoDTO>
    {
        public ProcessoValidator()
        {
            RuleFor(x => x.NumeroProcesso)
                .NotEmpty()
                .WithMessage("O número do processo não pode estar vazio.")
                .Matches(@"^\d{7}-\d{2}\.\d{4}\.\d{1,2}\.\d{2}\.\d{4}$")
                .WithMessage("O número do processo deve estar no formato correto: AAAAAAA-BB.CCCC.D.EE.FFFF");

            RuleFor(x => x.Distribuicao)
                .NotEmpty()
                .WithMessage("A distribuição não pode ser vazia.");

            RuleFor(x => x.Relator)
                .NotEmpty()
                .WithMessage("O relator não pode ser vazio.");

            RuleFor(x => x.Volume)
                .NotEmpty()
                .WithMessage("O volume não pode ser vazio.");

            RuleFor(x => x.Area)
                .NotEmpty()
                .WithMessage("A Área não pode ser vazia.");

            RuleFor(x => x.Assunto)
                .NotEmpty()
                .WithMessage("O Assunto não pode ser vazio.");


            RuleFor(x => x.Classe)
                .NotEmpty()
                .WithMessage("A classe não pode ser vazia.");
        }
    }
}
