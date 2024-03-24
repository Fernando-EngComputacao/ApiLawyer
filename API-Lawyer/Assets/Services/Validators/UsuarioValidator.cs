using API_Lawyer.Assets.Datas;
using API_Lawyer.Assets.Models.Usuario.dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API_Lawyer.Assets.Services.Validators
{
    public class UsuarioValidator : AbstractValidator<CreateUsuarioDTO>
    {
        private readonly UsuarioDbContext _context;

        public UsuarioValidator(UsuarioDbContext context)
        {
            _context = context;

            RuleFor(usuario => usuario.Username)
                .NotEmpty()
                .WithMessage("O nome de usuário é obrigatório.")
                .MustAsync(BeUniqueUsername)
                .WithMessage("Este nome de usuário já está em uso.");

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório.")
                .MustAsync(BeUniqueEmail)
                .WithMessage("Este email já está cadastrado.");

            RuleFor(usuario => usuario.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now)
                .WithMessage("A data de nascimento deve estar no passado.");

            RuleFor(usuario => usuario.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.")
                .Must(HasUpperCase).WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Must(HasLowerCase).WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Must(HasSpecialCharacter).WithMessage("A senha deve conter pelo menos um caractere especial.")
                .Must(HasNumber).WithMessage("A senha deve conter pelo menos um número.");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(u => u.UserName != username);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            // Verifique se o email já está cadastrado no banco de dados
            return await _context.Usuarios.AllAsync(u => u.Email != email);
        }

        private bool HasUpperCase(string password)
        {
            return Regex.IsMatch(password, @"[A-Z]");
        }

        private bool HasLowerCase(string password)
        {
            return Regex.IsMatch(password, @"[a-z]");
        }

        private bool HasNumber(string password)
        {
            return Regex.IsMatch(password, @"[0-9]+");
        }

        private bool HasSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[^a-zA-Z0-9]");
        }
    }
}
