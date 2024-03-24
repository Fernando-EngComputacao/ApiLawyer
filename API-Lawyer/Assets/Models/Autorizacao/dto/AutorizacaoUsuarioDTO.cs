using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Models.Autenticacao.dto
{
    public class AutorizacaoUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
