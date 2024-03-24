using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Assets.Models.Usuario.dto;
using API_Lawyer.Assets.Security.autorizacao;
using API_Lawyer.Assets.Services;
using API_Lawyer.Assets.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        /// <summary> Adiciona um usuário ao banco de dados + 🔓 LIVRE de Autenticação </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUsuario(CreateUsuarioDTO dto)
        {
            var result = await _service.CreateUsuario(dto);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary> Busca todos os usuários cadastrados no banco de dados </summary>
        [HttpGet]
        [RequireAuthentication]
        [Authorize(Policy = "standard")]
        public async Task<IActionResult> BuscaUsuarios([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _service.BuscaUsuarios(skip, take);
            return result != null ? Ok(result) : NotFound();
        }

    }
}
