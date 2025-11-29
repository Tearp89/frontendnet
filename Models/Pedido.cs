using System;
using System.Text.Json.Serialization;

namespace frontendnet.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [JsonPropertyName("carritoid")]
        public int CarritoId { get; set; }

        [JsonPropertyName("usuarioid")]
        public string UsuarioId { get; set; } = "";

        public decimal Total { get; set; }
        public string Estado { get; set; } = "";

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public PedidoCarrito Carrito { get; set; } = new();
    }
}
