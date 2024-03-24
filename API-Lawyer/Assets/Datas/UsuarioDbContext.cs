using API_Lawyer.Assets.Models.Usuario;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Datas
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) : base(opts)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}

