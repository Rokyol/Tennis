using System.ComponentModel.DataAnnotations.Schema;

namespace Tennis.Models.Response
{
    public class TorneoTerminadoResponse
    {
        public int IdJugador { get; set; }
        [ForeignKey(nameof(IdJugador))]
        public Jugador JugadorGanador { get; set; }
        public List<Partido>? Partidos { get; set; }

    }
}
