using API_Lawyer.Assets.Models.Transicao.dto;
using FluentValidation;

namespace API_Lawyer.Assets.Services.Validators
{
    public class TransicaoValidator : AbstractValidator<CreateTransicaoDTO>
    {
        public TransicaoValidator()
        {
            RuleFor(x => x.ProcessoId)
                .NotEmpty()
                .WithMessage("O ID do Processo não pode estar vazio.")
                .GreaterThan(0)
                .WithMessage("O Id do Processo deve ser maior que zero.");

            RuleFor(x => x.OrigemId)
                .NotEmpty()
                .WithMessage("O ID da Origem não pode estar vazio.")
                .GreaterThan(0)
                .WithMessage("O Id da Origem deve ser maior que zero.");
        }
    }
}
