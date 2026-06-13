using ApiInteligenteTareas.Models;
using ApiInteligenteTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace evaluacion_3.Controllers;

[ApiController]
[Route("api/ml")]
public class MlController : ControllerBase
{
    private readonly SentimientoService _sentimientoService;

    public MlController(SentimientoService sentimientoService)
    {
        _sentimientoService = sentimientoService;
    }

    [HttpPost("sentimiento")]
    public ActionResult<SentimientoResponseDto> ClasificarSentimiento([FromBody] SentimientoRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var sentimiento = _sentimientoService.Clasificar(request.Comentario);
        return Ok(new SentimientoResponseDto
        {
            Comentario = request.Comentario,
            Sentimiento = sentimiento,
        });
    }
}
