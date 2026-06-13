using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.Models;

public class SentimientoRequestDto
{
    [Required(ErrorMessage = "El comentario es obligatorio.")]
    public string Comentario { get; set; } = string.Empty;
}

public class SentimientoResponseDto
{
    public string Comentario { get; set; } = string.Empty;
    public string Sentimiento { get; set; } = string.Empty;
}
