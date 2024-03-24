using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Models.Usuario
{
    public class Usuario : IdentityUser
    {
        
        public DateTime DataNascimento { get; set; }

        public Usuario(): base() { }
        
    }
}
