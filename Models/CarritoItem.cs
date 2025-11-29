using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class CarritoItem
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Range(1, 100, ErrorMessage = "La cantidad debe ser entre 1 y 100.")]
    [Display(Name = "Cantidad")]
    public int Cantidad { get; set; } = 1;
}