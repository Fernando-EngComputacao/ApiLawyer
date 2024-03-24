using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Models.Usuario.dto
{
    public class CreateUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
