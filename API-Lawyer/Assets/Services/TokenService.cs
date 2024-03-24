
using API_Lawyer.Assets.Models.Usuario;
using API_Lawyer.Assets.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Lawyer.Assets.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AERTHJIHGFDER567YTFDY876GE44R5TT6YYY77U"));

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}