using frontendnet.Models;

namespace frontendnet.Services;

public class UsuariosClientService(HttpClient client)
{
    public async Task<List<Usuario>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Usuario>>("api/usuarios");
    }

    public async Task<Usuario?> GetAsync(string email)
    {
        var response = await client.GetAsync($"api/usuarios/{email}");
    
    // 2. Manejo de 404 (Not Found): Si el usuario no existe, devolvemos null
    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return null;
    }

    // 3. Manejo de otros errores (401 Unauthorized, 500 Internal Server Error, etc.)
    // EnsureSuccessStatusCode() lanza una HttpRequestException si el código no es de éxito (2xx)
    response.EnsureSuccessStatusCode(); 
    
    // 4. Deserialización: Solo si el código de estado es de éxito (2xx), intentamos leer el JSON
    return await response.Content.ReadFromJsonAsync<Usuario>();
        
    }

    public async Task PostAsync(UsuarioPwd usuario)
    {
        var response = await client.PostAsJsonAsync($"api/usuarios", usuario);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutAsync(Usuario usuario)
    {
        var response = await client.PutAsJsonAsync($"api/usuarios/{usuario.Email}", usuario);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string email)
    {
        var response = await client.DeleteAsync($"api/usuarios/{email}");
        response.EnsureSuccessStatusCode();
    }
}