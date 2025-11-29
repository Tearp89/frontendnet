using System.Text.Json.Serialization;

namespace frontendnet.Models
{
    public class PedidoProductoItem
    {
        public int Id { get; set; }              // id del producto
        public string Titulo { get; set; } = "";
        public decimal Precio { get; set; }

        [JsonPropertyName("carritoproducto")]
        public CarritoProductoInfo CarritoProducto { get; set; } = new();
    }
}
