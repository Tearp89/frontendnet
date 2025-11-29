using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet;

public class AuthController(AuthClientService auth, UsuariosClientService usuarios) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View(new Login
    {
        Email = "",
        Password = ""
    });
    }

    [HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<IActionResult> IndexAsync(Login model)
{
    if (ModelState.IsValid)
    {
        try
        {
            // Esta función verifica en backend que el correo y contraseña sean válidos
            AuthUser token = await auth.ObtenerTokenAsync(model.Email, model.Password);

            var claims = new List<Claim>
            {
                // Todo esto se guarda en la Cookie
                new(ClaimTypes.Name, token.Email),
                new(ClaimTypes.GivenName, token.Nombre),
                new("jwt", token.Jwt),
                new(ClaimTypes.Role, token.Rol)
            };

            auth.IniciaSesionAsync(claims);

            // Usuario válido
            if (token.Rol == "Administrador")
            {
                return RedirectToAction("Index", "Productos");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("Email", "Credenciales no válidas. Inténtelo nuevamente.");
        }
    }

    return View(model);
}


[Authorize(Roles = "Administrador, Usuario")]
public async Task<IActionResult> SalirAsync()
{
    // Cierra la sesión
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    // Sino, se redirige a la página inicial
    return RedirectToAction("Index", "Auth");
}

// Dentro de la clase AuthController:

// GET: /Auth/Crear
// GET: /Auth/Crear
[AllowAnonymous]
public IActionResult Crear()
{
    return View(new UsuarioPwd
    {
        // Inicializa explícitamente todos los miembros requeridos (CS9035)
        Email = string.Empty,
        Password = string.Empty,
        Nombre = string.Empty,
        Rol = "Usuario" // O el valor que esperes para el autorregistro
    });
}

// POST: /Auth/CrearAsync
[HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(UsuarioPwd itemToCreate) // POST
    {
        itemToCreate.Rol = "Usuario"; // Sobrescribe siempre el rol
        
        if (ModelState.IsValid)
        {
            try
            {
                // Usamos el nuevo método de servicio para el registro público
                await auth.RegistrarAsync(itemToCreate); 
                return RedirectToAction(nameof(Index)); // Redirige a login
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError("Email", "El correo ya existe o los datos son inválidos.");
                    return View(itemToCreate);
                }
            }
        }
        
        ModelState.AddModelError("Email", "No fue posible registrar la cuenta. Inténtelo nuevamente.");
        return View(itemToCreate);
    }
}