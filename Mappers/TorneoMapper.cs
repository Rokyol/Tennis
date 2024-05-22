using Tennis.Models.Response;
using Tennis.Models;

namespace Tennis.Mappers
{
    public static class TorneoMapper
    {
        public static TorneoTerminadoResponse ToTorneoTerminadoResponse(this Torneo torneo)
        {
            return new TorneoTerminadoResponse
            {
                JugadorGanador = torneo.JugadorW,
                Partidos = torneo.Partido
            };
        }
    }
}
