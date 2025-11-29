using System.Net.Http.Json;
using frontendnet.Models;

public class CarritoClienteService
{
    private readonly HttpClient client;

    public CarritoClienteService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CarritoData> GetAsync()
    {
        var data = await client.GetFromJsonAsync<CarritoData>("api/carrito");
        return data ?? new CarritoData();
    }

    public async Task AddItemAsync(int productoId, int cantidad = 1)
    {
        var payload = new { productoId, cantidad };
        var response = await client.PostAsJsonAsync("api/carrito/items", payload);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateItemAsync(int itemId, int cantidad)
    {
        var payload = new { cantidad };
        var response = await client.PutAsJsonAsync($"api/carrito/items/{itemId}", payload);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveItemAsync(int itemId)
    {
        var response = await client.DeleteAsync($"api/carrito/items/{itemId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ClearAsync()
    {
        var response = await client.DeleteAsync("api/carrito");
        response.EnsureSuccessStatusCode();
    }

    public async Task CheckoutAsync()
{
    var response = await client.PatchAsync("api/carrito", null);
    response.EnsureSuccessStatusCode();
}

}
