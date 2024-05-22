using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tennis.Mappers;
using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorService _jugadorService;
        public JugadorController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }
        [HttpPost]
        public async Task<JugadorResponse> CreateJugador (JugadorRequest jugadorRequest)
        {
            Jugador response = await _jugadorService.CreateJugador(jugadorRequest);
            var jugador = response.ToJugadorResponse();
            return jugador;
        }
        [HttpPost("MultiCreate")]
        public async Task<bool> MultiCreate(List<JugadorRequest> jugadoresRequest)
        {
            return await _jugadorService.AddRangeJugador(jugadoresRequest);
        }
        [HttpPut]
        public async Task<JugadorResponse> EditJugador (Jugador jugador)
        {
            Jugador response = await _jugadorService.EditJugador(jugador);
            return response.ToJugadorResponse();
        }
        [HttpPost, Route("Deleted/{dni}")]
        public async Task<bool> DeletedJugador(int dni)
        {
            return await _jugadorService.Deleted(dni);
        }

    }
}
