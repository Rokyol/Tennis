using Microsoft.EntityFrameworkCore;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Models;
using Tennis.Models.Entities;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class TorneoService : ITorneoService
    {
        private readonly TennisContext _tennisContext;
    
        public TorneoService (TennisContext tennisContext)
        {
            _tennisContext = tennisContext;
        }

        public async Task<bool> CreateTorneo(Torneo torneo, int userId)
        {
            if(_tennisContext.Set<Torneo>().Where((t) => t.Nombre.ToLower() == torneo.Nombre.ToLower()).FirstOrDefault() != null) 
            {
                throw new BadHttpRequestException($"Ya existe un torneo con el nombre '{torneo.Nombre}'");
            }
            if (torneo.TorneoJugador == null || !TorneoValidation.IsPowerOfTwo(torneo.TorneoJugador.Count))
            {
                throw new BadHttpRequestException("La cantidad de jugadores debe ser una potencia de dos.");
            }
            torneo.CreatedByUserId = userId;
            if (torneo.Genero.ToLower().Trim() != "femenino" && torneo.Genero.ToLower().Trim() != "masculino")
            {
                throw new BadHttpRequestException("El torneo solo puede ser 'masculino' o 'femenino'");
            }
            torneo.TorneoJugador.ForEach((tj) =>
            {
                var jugador = _tennisContext.Set<Jugador>().Where((e) => e.Dni == tj.JugadorId).FirstOrDefault();
                if (jugador != null)
                {
                    if (jugador.Genero.ToLower().Trim() != torneo.Genero.ToLower().Trim())
                    {
                        throw new BadHttpRequestException($"El dni '{tj.JugadorId}' no corresponde al genero del torneo");
                    }
                    else
                    {
                        tj.JugadorId = jugador.Id;
                    }
                }
                else
                {
                    throw new BadHttpRequestException($"El dni '{tj.JugadorId}' no existe o no es válido");
                }
            });
            _tennisContext.Set<Torneo>().Attach(torneo);
            int resp =  await _tennisContext.SaveChangesAsync();
            return resp > 0;
        }
        public async Task<Torneo> GetTorneo(int id)
        {
            return await _tennisContext.Set<Torneo>().Where((e) => e.Id == id)
                                                     .Include((e) => e.TorneoJugador).ThenInclude((e) => e.Jugador)
                                                     .FirstOrDefaultAsync();
        }
        public async Task<Torneo> GetTorneoByNombre(string nombre)
        {
            return await _tennisContext.Set<Torneo>().Where((e) => e.Nombre == nombre)
                                                     .Include((e) => e.TorneoJugador).ThenInclude((e) => e.Jugador)
                                                     .FirstOrDefaultAsync();
        }
        public async Task<TorneoTerminadoResponse> IniciarTorneo(Torneo torneo)
        {
            TorneoTerminadoResponse resultado = new TorneoTerminadoResponse();
            if (torneo.Genero.ToLower() == "masculino")
            {
                 resultado = TorneoMasculinoIniciar(torneo);
            }
            else
            {
                 resultado =TorneoFemIniciar(torneo);
            }
            torneo.IdJugadorW = resultado.IdJugador;
            torneo.FechaTermino = DateTime.Now;
            _tennisContext.Set<Torneo>().Update(torneo);
            await _tennisContext.SaveChangesAsync();
            Torneo result = await _tennisContext.Set<Torneo>().Where(e => e.Id == torneo.Id)
                                                              .Include(e => e.JugadorW)
                                                              .Include(e => e.Partido)
                                                              .AsNoTracking()
                                                              .FirstOrDefaultAsync();
            return result.ToTorneoTerminadoResponse();
        }

        public TorneoTerminadoResponse TorneoMasculinoIniciar(Torneo torneo)
        {
            List<TorneoJugador> torneoJugador = torneo.TorneoJugador;
            while (torneoJugador.Count > 1)
            {
                var ganadoresRonda = new List<TorneoJugador>();

                for (int i = 0; i < torneoJugador.Count; i += 2)
                {
                    var jugador1 = torneoJugador[i];
                    var jugador2 = torneoJugador[i + 1];

                    var ganadorEnfrentamiento = SimularEnfrentamientoMasculino(jugador1, jugador2);
                    var partido = new Partido();
                    partido.IdTorneo = torneo.Id;
                    partido.IdJugadorL = ganadorEnfrentamiento.JugadorId == jugador1.JugadorId ? jugador2.JugadorId : jugador1.JugadorId;
                    partido.IdJugadorW = ganadorEnfrentamiento.JugadorId;
                    _tennisContext.Set<Partido>().Add(partido);
                    _tennisContext.SaveChanges();
                    ganadoresRonda.Add(ganadorEnfrentamiento);
                }
                torneoJugador = ganadoresRonda;
            }

            var ganador = new TorneoTerminadoResponse();
            ganador.IdJugador = torneoJugador[0].JugadorId;
            ganador.JugadorGanador = torneoJugador[0].Jugador;
            return ganador;
        }

        public TorneoTerminadoResponse TorneoFemIniciar(Torneo torneo)
        {
            List<TorneoJugador> torneoJugador = torneo.TorneoJugador;
            while (torneoJugador.Count > 1)
            {
                var ganadoresRonda = new List<TorneoJugador>();

                for (int i = 0; i < torneoJugador.Count; i += 2)
                {
                    var jugador1 = torneoJugador[i];
                    var jugador2 = torneoJugador[i + 1];

                    var ganadorEnfrentamiento = SimularEnfrentamientoFem(jugador1, jugador2);
                    var partido = new Partido();
                    partido.IdTorneo = torneo.Id;
                    partido.IdJugadorL = ganadorEnfrentamiento.JugadorId == jugador1.JugadorId ? jugador2.JugadorId : jugador1.JugadorId;
                    partido.IdJugadorW = ganadorEnfrentamiento.JugadorId;
                    _tennisContext.Set<Partido>().Add(partido);
                    _tennisContext.SaveChanges();
                    ganadoresRonda.Add(ganadorEnfrentamiento);
                }
                torneoJugador = ganadoresRonda;
            }

            var ganador = new TorneoTerminadoResponse();
            ganador.IdJugador = torneoJugador[0].JugadorId;
            ganador.JugadorGanador = torneoJugador[0].Jugador;
            return ganador;
        }
        public TorneoJugador SimularEnfrentamientoMasculino (TorneoJugador jug1, TorneoJugador jug2)
        {
            int puntaje1 = CalcularPuntajeMasculino(jug1.Jugador);
            int puntaje2 = CalcularPuntajeMasculino(jug2.Jugador);
            
            if (puntaje1 == puntaje2)
            {
                if (jug1.Jugador.Suerte > jug2.Jugador.Suerte)
                {
                    puntaje1 += 1;
                }
                else
                {
                    puntaje2 += 1;
                }
            }
            return puntaje1 > puntaje2 ? jug1 : jug2;
        }

        private int CalcularPuntajeMasculino(Jugador jugador)
        {
            int puntaje = jugador.Habilidad + jugador.Fuerza + jugador.Velocidad;
            return puntaje;
        }
        public TorneoJugador SimularEnfrentamientoFem(TorneoJugador jug1, TorneoJugador jug2)
        {
            int puntaje1 = CalcularPuntajeFem(jug1.Jugador);
            int puntaje2 = CalcularPuntajeFem(jug2.Jugador);
            if (puntaje1 == puntaje2)
            {
                if (jug1.Jugador.Suerte > jug2.Jugador.Suerte)
                {
                    puntaje1 += 1;
                }
                else
                {
                    puntaje2 += 1;
                }
            }
            return puntaje1 > puntaje2 ? jug1 : jug2;
        }
        private int CalcularPuntajeFem(Jugador jugador)
        {
            int puntaje = jugador.Habilidad + jugador.Reaccion;
            return puntaje;
        }

        public async Task<List<Torneo>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Torneo> torneos = await _tennisContext.Set<Torneo>()
                .Where(t => t.FechaTermino.Value.Date >= fechaDesde.Date && t.FechaTermino.Value.Date <= fechaHasta.Date)
                .Include(t => t.TorneoJugador).ThenInclude(e => e.Jugador)
                .Include(e => e.JugadorW)
                .Include(e => e.Partido)
                .AsNoTracking()
                .ToListAsync();
            return torneos;
        }

    }
}
