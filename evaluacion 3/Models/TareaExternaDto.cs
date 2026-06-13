namespace ApiInteligenteTareas.Models;

public class TareaExternaDto
{
    public int ExternalId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool Completado { get; set; }
}

internal class JsonPlaceholderTodo
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; }
}
