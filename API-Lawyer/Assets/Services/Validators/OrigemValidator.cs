using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Origem.dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Services.Validators
{
    public class OrigemValidator : AbstractValidator<CreateOrigemDTO>
    {
        private readonly LawyerContext _context;
        public OrigemValidator(LawyerContext context)
        {
            _context = context;

            RuleFor(x => x.Local)
                .NotEmpty()
                .WithMessage("O Local não pode ser nulo")
                .MustAsync(ExistirLocal)
                .WithMessage("O Local já existe no banco de dados");    
        }

        private async Task<bool> ExistirLocal(string local, CancellationToken cancellationToken)
        {
            // Verificar se o local já existe no banco de dados
            return !await _context.Origens.AnyAsync(o => o.Local == local);
        }
    }
}
