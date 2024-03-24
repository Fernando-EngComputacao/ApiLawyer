using API_Lawyer.Assets.Datas;
using API_Lawyer.Assets.Models.Autenticacao.dto;
using API_Lawyer.Assets.Models.Usuario;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API_Lawyer.Assets.Services.Validators
{
    public class AutorizacaoValidator : AbstractValidator<AutorizacaoUsuarioDTO>
    {
        private readonly UsuarioDbContext _context;
        private readonly SignInManager<Usuario> _signInManager;
        public AutorizacaoValidator(UsuarioDbContext context, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _signInManager = signInManager;

            RuleFor(usuario => usuario.Username)
                .NotEmpty()
                .WithMessage("O nome de usuário é obrigatório.");
            //.MustAsync(BeUniqueUsername)

            RuleFor(usuario => usuario.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");

            RuleFor(x => x) // Validate both Username and Password at once
                .Must(x => ValidateCredentials(x.Username, x.Password))
                .WithMessage("Usuário ou senha incorretos.");

            
        }
        private bool ValidateCredentials(string username, string password)
        {
            return _signInManager.PasswordSignInAsync(username, password, false, false).Result.Succeeded;
        }
    }
}
