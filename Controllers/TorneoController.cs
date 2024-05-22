using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TorneoController : ControllerBase
    {
        private readonly ITorneoService _torneoService;
        public TorneoController(ITorneoService torneoService)
        {
            _torneoService = torneoService;
        }
        [HttpPost]
        public async Task<ActionResult<Torneo>> CreateTorneo (Torneo torneo)
        {
            var Rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
            if (Rol?.ToLower() == "admin")
            {
                if (torneo.GetType() == typeof(Torneo))
                {
                    var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                    var response = await _torneoService.CreateTorneo(torneo, int.Parse(userId));
                    return CreatedAtAction(nameof(GetTorneo), new { id = torneo.Id }, torneo);
                }
                else
                {
                    throw new Exception("Solo se admite datos para crear el torneo");

                }
            }
            else
            {
                    return new ForbidResult();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Torneo>> GetTorneo(int id)
        {
            var Rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
            if (Rol?.ToLower() == "admin")
            {
                var torneo = await _torneoService.GetTorneo(id);

                if (torneo == null)
                {
                    throw new Exception("No se encontro el torneo");
                }

                return torneo;
            }
            else
            {
                throw new Exception("Solo se admite datos para crear el torneo");

            }
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Torneo>> GetTorneoByNombre(string nombre)
        {
            var torneo = await _torneoService.GetTorneoByNombre(nombre);

            if (torneo == null)
            {
                throw new Exception("No se encontro el torneo");
            }

            return torneo;
        }
        [HttpPost("IniciarTorneo/{nombre}")]
        public async Task<ActionResult<TorneoTerminadoResponse>> IniciarTorneo(string nombre)
        {
            var Rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
            if (Rol?.ToLower() == "admin")
            {
                var actionResult = await GetTorneoByNombre(nombre);
                if (actionResult.Result is NotFoundResult)
                {
                    return NotFound($"El torneo con nombre: '{nombre}' no existe.");
                }

                var torneo = actionResult.Value;
                if (torneo?.JugadorW != null)
                {
                    return NotFound($"El torneo ya termino, el ganador es {torneo.JugadorW.Nombre + " " + torneo.JugadorW.Apellido}");
                }
                TorneoTerminadoResponse response = await _torneoService.IniciarTorneo(torneo);
                return response;
            }
            else
            {
                throw new Exception("Solo se admite datos para crear el torneo");

            }

        }
        [HttpGet("getTorneos/{fechaDesde}/{fechaHasta}")]
        public async Task<ActionResult<List<Torneo>>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta)

        {
            try
            {
                if (fechaHasta > DateTime.Today)
                {
                    return BadRequest("La fecha hasta no puede ser mayor que la fecha actual.");
                }

                var torneos = await _torneoService.GetTorneosByFecha(fechaDesde, fechaHasta);
                return Ok(torneos);
            }catch (Exception ex)
            {
                if(ex.Message == "Type of object ")
                {
                    return BadRequest("La fecha tiene que se del tipo 'yyyy/mm/dd'");
                }
                return BadRequest(ex.Message);
            }
        }
    }
}
