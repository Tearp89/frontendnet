using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet;

[Authorize(Roles = "Usuario")]
public class ComprarController : Controller
{
    private readonly CarritoClienteService _carrito;
    private readonly ProductosClientService _productos;

    public ComprarController(CarritoClienteService carrito, ProductosClientService productos)
    {
        _carrito = carrito;
        _productos = productos;
    }

    // LISTADO DE PRODUCTOS
    public async Task<IActionResult> Index(string? s)
    {
        // Obtener productos desde el API usando ProductosClientService
        var productos = await _productos.GetAsync(s);  // ← List<Producto>
        return View(productos);
    }

    // VISTA DEL CARRITO
    public async Task<IActionResult> Carrito()
    {
        var carrito = await _carrito.GetAsync();
        return View("~/Views/Carrito/Index.cshtml", carrito);
    }


    // ------------------ MÉTODOS DEL CARRITO ------------------

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AgregarItem(int productoId, int cantidad = 1)
    {
        await _carrito.AddItemAsync(productoId, cantidad);

        // Si viene por AJAX (modal tipo Steam) solo regresamos 200 OK
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Ok();
        }

        // Si es un post normal, seguimos como antes: ir al carrito
        return RedirectToAction("Carrito");
    }


    [HttpPost]
    public async Task<IActionResult> ActualizarItem(int id, int cantidad)
    {
        await _carrito.UpdateItemAsync(id, cantidad);
        return RedirectToAction("Carrito");
    }

    [HttpPost]
    public async Task<IActionResult> EliminarItem(int id)
    {
        await _carrito.RemoveItemAsync(id);
        return RedirectToAction("Carrito");
    }

    [HttpPost]
    public async Task<IActionResult> Vaciar()
    {
        await _carrito.ClearAsync();
        TempData["Mensaje"] = "El carrito se vació correctamente";
        return RedirectToAction("Carrito");
    }


    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        await _carrito.CheckoutAsync();
        TempData["Mensaje"] = "Compra realizada con éxito";
        return RedirectToAction("Carrito");
    }

    public async Task<IActionResult> Detalle(int id)
    {
        var producto = await _productos.GetByIdAsync(id);
        if (producto == null)
            return NotFound();

        return View(producto); // Views/Comprar/Detalle.cshtml
    }

}
