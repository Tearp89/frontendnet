using System.Text.Json.Serialization;

namespace frontendnet.Models
{
    public class PedidoCarrito
    {
        public int Id { get; set; }
        public string Estado { get; set; } = "";

        [JsonPropertyName("productos")]
        public List<PedidoProductoItem> Productos { get; set; } = new();
    }
}
