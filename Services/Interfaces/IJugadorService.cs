using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Models.Response;

namespace Tennis.Services.Interfaces
{
    public interface IJugadorService
    {
        Task<Jugador> CreateJugador(JugadorRequest jugadorRequest);
        Task<Jugador> EditJugador(Jugador jugador);
        Task<bool> AddRangeJugador(List<JugadorRequest> jugadores);
        Task<bool> Deleted(int dni);
    }
}
