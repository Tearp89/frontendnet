using System.Security.Claims;
using frontendnet.Services;
using Microsoft.Extensions.DependencyInjection; // Nueva dependencia para GetRequiredService

namespace frontendnet.Middlewares;

// El constructor solo inyecta el HttpContextAccessor
public class RefrescaTokenDelegatingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        // Revisa si el servidor nos envió un nuevo token
        if (response.Headers.Contains("Set-Authorization"))
        {
            // SOLUCIÓN: Obtenemos el AuthClientService del contexto de la solicitud.
            // Esto rompe el ciclo de dependencia en el momento de la configuración (startup).
            var auth = httpContextAccessor.HttpContext?.RequestServices.GetRequiredService<AuthClientService>();

            if (auth != null)
            {
                string jwt = response.Headers.GetValues("Set-Authorization").FirstOrDefault()!;
                var claims = new List<Claim>
                {
                    // Todo esto se guarda en la Cookie
                    new(ClaimTypes.Name, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!),
                    new(ClaimTypes.GivenName, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName)!),
                    new("jwt", jwt),
                    new(ClaimTypes.Role, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!)
                };
                
                // Usamos la instancia resuelta
                auth.IniciaSesionAsync(claims);
            }
        }

        return response;
    }
}