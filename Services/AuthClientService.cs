using frontendnet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace frontendnet.Services;

public class AuthClientService(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    public async Task<AuthUser> ObtenerTokenAsync(string email, string password)
    {
        Login usuario = new() { Email = email, Password = password };
        // Realizo la llamada a la Web API
        var response = await client.PostAsJsonAsync("api/auth", usuario);
        var token = await response.Content.ReadFromJsonAsync<AuthUser>();

        return token!;
    }

    public async Task IniciaSesionAsync(List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();
        await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    public async Task RegistrarAsync(UsuarioPwd usuario) // <-- NUEVO MÉTODO
    {
        // Llama al nuevo endpoint de registro público
        var response = await client.PostAsJsonAsync("api/auth/registro", usuario);
        response.EnsureSuccessStatusCode();
    }
}