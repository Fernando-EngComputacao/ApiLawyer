using API_Lawyer.Assets.Models.Autenticacao.dto;
using API_Lawyer.Assets.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutorizacaoService _service;

        public AutenticacaoController(AutorizacaoService service)
        {
            _service = service;
        }

        /// <summary> 🔑 Autentica um usuário (login) + 🔓 LIVRE de Autenticação  </summary>
        [HttpPost]
        public async Task<IActionResult> Autenticacao(AutorizacaoUsuarioDTO dto)
        {
            var token = await _service.Login(dto);
            return Ok(token);
        }

    }
}
