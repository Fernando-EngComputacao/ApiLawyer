using API_Lawyer.Assets.Model.Movimentacao.dto;
using FluentValidation;

namespace API_Lawyer.Assets.Services.Validators
{
    public class MovimentacaoValidator : AbstractValidator<CreateMovimentacaoDTO>
    {
        public MovimentacaoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("A descrição não pode ser nula");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("A data não pode ser nula");
        }
    }
}
