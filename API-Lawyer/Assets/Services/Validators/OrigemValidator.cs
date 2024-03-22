using API_Lawyer.Assets.Model.Origem.dto;
using FluentValidation;

namespace API_Lawyer.Assets.Services.Validators
{
    public class OrigemValidator : AbstractValidator<CreateOrigemDTO>
    {
        public OrigemValidator()
        {
            RuleFor(x => x.Local)
                .NotEmpty()
                .WithMessage("O Local não pode ser nulo");
        }
    }
}
