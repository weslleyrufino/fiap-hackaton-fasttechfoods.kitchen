using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace FastTechFoods.Kitchen.API.Security;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    // guarda o handler original pra delegar quando for 200 ou 401
    private readonly AuthorizationMiddlewareResultHandler DefaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult result)
    {
        if (result.Forbidden) // usuário autenticado, mas sem permissão
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response
                         .WriteAsJsonAsync(new
                         {
                             error = "You do not have permission to access this resource."
                         });
            return;
        }

        // 401 ou sucesso: usa o comportamento padrão
        await DefaultHandler.HandleAsync(next, context, policy, result);
    }
}