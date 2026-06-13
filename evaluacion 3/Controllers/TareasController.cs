using ApiInteligenteTareas.Models;
using ApiInteligenteTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace evaluacion_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly TareaService _tareaService;

    public TareasController(TareaService tareaService)
    {
        _tareaService = tareaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaResponseDto>>> GetAll()
    {
        var tareas = await _tareaService.ObtenerTodas();
        return Ok(tareas.Select(MapToResponse));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TareaResponseDto>> GetById(int id)
    {
        var tarea = await _tareaService.ObtenerPorId(id);
        if (tarea is null)
        {
            return NotFound();
        }

        return Ok(MapToResponse(tarea));
    }

    [HttpPost]
    public async Task<ActionResult<TareaResponseDto>> Create([FromBody] TareaCreateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.FechaVencimiento.Date < DateTime.Today)
        {
            ModelState.AddModelError(nameof(request.FechaVencimiento), "La fecha de vencimiento no puede ser menor a la fecha actual.");
            return BadRequest(ModelState);
        }

        var tarea = new Tarea
        {
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            Estado = request.Estado!.Value,
            Prioridad = request.Prioridad!.Value,
            FechaVencimiento = request.FechaVencimiento,
        };

        var created = await _tareaService.Crear(tarea);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToResponse(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TareaResponseDto>> Update(int id, [FromBody] TareaUpdateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.FechaVencimiento.Date < DateTime.Today)
        {
            ModelState.AddModelError(nameof(request.FechaVencimiento), "La fecha de vencimiento no puede ser menor a la fecha actual.");
            return BadRequest(ModelState);
        }

        var existing = await _tareaService.ObtenerPorId(id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Titulo = request.Titulo;
        existing.Descripcion = request.Descripcion;
        existing.Estado = request.Estado!.Value;
        existing.Prioridad = request.Prioridad!.Value;
        existing.FechaVencimiento = request.FechaVencimiento;

        await _tareaService.Actualizar(existing);
        return Ok(MapToResponse(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _tareaService.ObtenerPorId(id);
        if (existing is null)
        {
            return NotFound();
        }

        await _tareaService.Eliminar(existing);
        return NoContent();
    }

    private static TareaResponseDto MapToResponse(Tarea tarea)
    {
        return new TareaResponseDto
        {
            Id = tarea.Id,
            Titulo = tarea.Titulo,
            Descripcion = tarea.Descripcion,
            Estado = tarea.Estado,
            Prioridad = tarea.Prioridad,
            FechaCreacion = tarea.FechaCreacion,
            FechaVencimiento = tarea.FechaVencimiento,
        };
    }
}
