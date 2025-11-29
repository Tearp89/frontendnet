using System.Net.Http.Json;
using frontendnet.Models;

namespace frontendnet.Services
{
    public class PedidoClienteService
    {
        private readonly HttpClient _client;

        public PedidoClienteService(HttpClient client)
        {
            _client = client;
        }

        // GET: api/pedidos  (lista del usuario o todos si es Admin)
        public async Task<List<Pedido>> GetPedidosAsync()
        {
            var result = await _client.GetFromJsonAsync<List<Pedido>>("api/pedido");
            return result ?? new List<Pedido>();
        }

        // GET: api/pedidos/5
        public async Task<Pedido?> GetPedidoAsync(int id)
        {
            return await _client.GetFromJsonAsync<Pedido>($"api/pedido/{id}");
        }

        public async Task<Pedido?> ComprarAhoraAsync(int productoId, int cantidad = 1)
        {
            var payload = new { productoId, cantidad };
            var resp = await _client.PostAsJsonAsync("api/pedido", payload);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Pedido>();
        }

    }
}
