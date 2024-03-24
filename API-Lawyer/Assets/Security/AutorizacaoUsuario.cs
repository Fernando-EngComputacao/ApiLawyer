using API_Lawyer.Assets.Models.Usuario.secret;
using Microsoft.AspNetCore.Authorization;

namespace API_Lawyer.Assets.Security
{
    public class AutorizacaoUsuario : AuthorizationHandler<UsuarioValido>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UsuarioValido requirement)
        {
            var timeStampClaim = context.User.FindFirst(claim => claim.Type == ("loginTimestamp"));

            if (timeStampClaim != null)
            {
                var tokenDate = Convert.ToDateTime(timeStampClaim.Value);
                var currentDate = DateTime.Now;
                var difference = tokenDate - currentDate;

                // Valida as 2h de duração do TOKEN
                if (difference.TotalHours > 0)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
