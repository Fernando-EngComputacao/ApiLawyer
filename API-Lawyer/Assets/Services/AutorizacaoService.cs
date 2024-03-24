using API_Lawyer.Assets.Models.Autenticacao.dto;
using API_Lawyer.Assets.Models.Usuario;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Lawyer.Assets.Services
{
    public class AutorizacaoService : IAutorizacaoService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly TokenService _tokenService;
        private readonly AutorizacaoValidator _validator;

        public AutorizacaoService(SignInManager<Usuario> signInManager, TokenService tokenService, AutorizacaoValidator validator)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task<string> Login(AutorizacaoUsuarioDTO dto)
        {
            //Validação
            ValidarRequestAutorizacao(dto);

            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            var usuario =  _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedUserName == dto.Username.ToUpper());
            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

        private void ValidarRequestAutorizacao(AutorizacaoUsuarioDTO dto)
        {
            var validationResult = _validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validationResult.Errors);
                throw new LawyerException(errors, HttpStatusCode.BadRequest);
            }
        }

    }
}
