// Models/CarritoData.cs (Crea este archivo o clase)
using System.Collections.Generic;

namespace frontendnet.Models;

public class CarritoData 
{
    public int? Id { get; set; }
    
    // Debe coincidir con 'items' del JSON de backend
    public List<CarritoProductoItem> Items { get; set; } = []; 
    
    // Debe coincidir con 'total' del JSON de backend
    public decimal Total { get; set; }
}