using System.Text.Json.Serialization;

public class CarritoProductoItem
{
    public int ProductoId { get; set; }
    public string Titulo { get; set; } = "";
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string? ArchivoId { get; set; }

    // ESTA ES LA CLAVE:
    [JsonPropertyName("carritoproducto")]
    public CarritoProductoInfo CarritoProducto { get; set; } = new();
}
