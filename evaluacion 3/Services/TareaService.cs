using ApiInteligenteTareas.Data;
using ApiInteligenteTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInteligenteTareas.Services;

public class TareaService
{
    private readonly AppDbContext _context;

    public TareaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tarea>> ObtenerTodas()
    {
        return await _context.Tareas.ToListAsync();
    }

    public async Task<Tarea?> ObtenerPorId(int id)
    {
        return await _context.Tareas.FindAsync(id);
    }

    public async Task<Tarea> Crear(Tarea tarea)
    {
        tarea.FechaCreacion = DateTime.Now;

        _context.Tareas.Add(tarea);

        await _context.SaveChangesAsync();

        return tarea;
    }

    public async Task Actualizar(Tarea tarea)
    {
        _context.Tareas.Update(tarea);

        await _context.SaveChangesAsync();
    }

    public async Task Eliminar(Tarea tarea)
    {
        _context.Tareas.Remove(tarea);

        await _context.SaveChangesAsync();
    }
}