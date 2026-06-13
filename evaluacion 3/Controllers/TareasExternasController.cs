using ApiInteligenteTareas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace evaluacion_3.Controllers;

[ApiController]
[Route("api/tareas-externas")]
public class TareasExternasController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public TareasExternasController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaExternaDto>>> GetAll()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al obtener tareas externas.");
            }

            var todos = await response.Content.ReadFromJsonAsync<List<JsonPlaceholderTodo>>();
            if (todos is null)
            {
                return StatusCode(502, "Error al deserializar la respuesta de la API externa.");
            }

            return Ok(todos.Select(t => new TareaExternaDto
            {
                ExternalId = t.Id,
                Titulo = t.Title,
                Completado = t.Completed,
            }));
        }
        catch
        {
            return StatusCode(502, "La API externa no respondió correctamente.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TareaExternaDto>> GetById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al obtener la tarea externa.");
            }

            var todo = await response.Content.ReadFromJsonAsync<JsonPlaceholderTodo>();
            if (todo is null)
            {
                return StatusCode(502, "Error al deserializar la respuesta de la API externa.");
            }

            return Ok(new TareaExternaDto
            {
                ExternalId = todo.Id,
                Titulo = todo.Title,
                Completado = todo.Completed,
            });
        }
        catch
        {
            return StatusCode(502, "La API externa no respondió correctamente.");
        }
    }
}
