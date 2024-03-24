namespace API_Lawyer.Assets.Security.autorizacao;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class RequireAuthenticationAttribute : Attribute
{
}