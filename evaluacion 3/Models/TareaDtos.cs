using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.Models;

public class TareaCreateDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public EstadoTarea? Estado { get; set; }

    [Required(ErrorMessage = "La prioridad es obligatoria.")]
    public PrioridadTarea? Prioridad { get; set; }

    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
    public DateTime FechaVencimiento { get; set; }
}

public class TareaUpdateDto : TareaCreateDto
{
}

public class TareaResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public EstadoTarea Estado { get; set; }
    public PrioridadTarea Prioridad { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
