using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Models.Response;

namespace Tennis.Mappers
{
    public static class JugadorMapper
    {
        public static List<Jugador> ToJugadores(this List<JugadorRequest> jugadoresRequest)
        {
            return jugadoresRequest.Select(jugadorRequest => new Jugador
            {
                Dni = jugadorRequest.Dni,
                Nombre = jugadorRequest.Nombre,
                Apellido = jugadorRequest.Apellido,
                Nacimiento = jugadorRequest.Nacimiento,
                Genero = jugadorRequest.Genero,
                Habilidad = jugadorRequest.Habilidad,
                Suerte = jugadorRequest.Suerte,
                Fuerza = jugadorRequest.Fuerza,
                Velocidad = jugadorRequest.Velocidad,
                Reaccion = jugadorRequest.Reaccion,
                Activo = jugadorRequest.Activo
            }).ToList();
        }
        public static Jugador ToJugador(this JugadorRequest jugadorRequest)
        {
            return new Jugador
            {
                Dni = jugadorRequest.Dni,
                Nombre = jugadorRequest.Nombre,
                Apellido = jugadorRequest.Apellido,
                Nacimiento = jugadorRequest.Nacimiento,
                Genero = jugadorRequest.Genero,
                Habilidad = jugadorRequest.Habilidad,
                Suerte = jugadorRequest.Suerte,
                Fuerza = jugadorRequest.Fuerza,
                Velocidad = jugadorRequest.Velocidad,
                Reaccion = jugadorRequest.Reaccion,
                Activo = jugadorRequest.Activo
            };
        }
        public static JugadorResponse ToJugadorResponse(this Jugador jugadorRequest)
        {
            return new JugadorResponse
            {
                Dni = jugadorRequest.Dni,
                Nombre = jugadorRequest.Nombre,
                Apellido = jugadorRequest.Apellido,
                Nacimiento = jugadorRequest.Nacimiento,
                Genero = jugadorRequest.Genero,
                Habilidad = jugadorRequest.Habilidad,
                Suerte = jugadorRequest.Suerte,
                Fuerza = jugadorRequest.Fuerza,
                Velocidad = jugadorRequest.Velocidad,
                Reaccion = jugadorRequest.Reaccion,
                Activo = jugadorRequest.Activo
            };
        }
    }
}
