using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet.Controllers
{
    [Authorize(Roles = "Usuario,Administrador")]
    public class PedidosController : Controller
    {
        private readonly PedidoClienteService _service;

        public PedidosController(PedidoClienteService service)
        {
            _service = service;
        }

        // GET: /Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _service.GetPedidosAsync();
            return View(pedidos);
        }

        // GET: /Pedidos/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var pedido = await _service.GetPedidoAsync(id);
            if (pedido == null)
                return NotFound();

            return View(pedido);
        }
    }
}
