using API_Lawyer.Assets.Datas;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Assets.Models.Usuario;
using API_Lawyer.Assets.Models.Usuario.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Lawyer.Assets.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UsuarioDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly UsuarioValidator _validator;


        public UsuarioService(IMapper mapper, UsuarioDbContext context, UserManager<Usuario> userManager, UsuarioValidator validator)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<ReadUsuarioDTO> CreateUsuario(CreateUsuarioDTO dto)
        {
            ValidarRequestUsuario(dto);
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult result = await _userManager.CreateAsync(usuario, dto.Password);
            return (result.Succeeded ? _mapper.Map<ReadUsuarioDTO>(usuario): null);
        }

        public async Task<IEnumerable<ReadUsuarioDTO>> BuscaUsuarios([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadUsuarioDTO>>(await _context.Users.Skip(skip).Take(take).ToListAsync());
        }


        private void ValidarRequestUsuario(CreateUsuarioDTO dto)
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
