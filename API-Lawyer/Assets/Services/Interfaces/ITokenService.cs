using API_Lawyer.Assets.Models.Usuario;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
