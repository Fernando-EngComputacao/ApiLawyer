using API_Lawyer.Assets.Models.Usuario.dto;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<ReadUsuarioDTO>> BuscaUsuarios([FromQuery] int skip = 0, [FromQuery] int take = 10);
        Task<ReadUsuarioDTO> CreateUsuario(CreateUsuarioDTO dto);
    }
}
