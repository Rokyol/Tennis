using Microsoft.EntityFrameworkCore;
using Tennis.API.Models.Request;
using Tennis.Mappers;
using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class JugadorService : IJugadorService
    {
        private readonly TennisContext _tennisContext;

        public JugadorService(TennisContext tennisContext)
        {
            _tennisContext = tennisContext;
        }

        public async Task<Jugador> CreateJugador(JugadorRequest jugador)
        {
            Jugador? jugadorExiste = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni && e.Activo == true).FirstOrDefaultAsync();
            if (jugadorExiste != null)
                throw new Exception($"El jugador con dni '{jugadorExiste.Dni}' ya existe");
            _tennisContext.Set<Jugador>().Add(jugador.ToJugador());
            await _tennisContext.SaveChangesAsync();
            Jugador? response = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni).FirstOrDefaultAsync();
            return response;
        }
        public async Task<bool> AddRangeJugador(List<JugadorRequest> jugadores)
        {
            jugadores.ForEach((jugador) =>
            {
                Jugador? jugadorExiste =  _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni && e.Activo == true).FirstOrDefault();
                if (jugadorExiste != null)
                    throw new Exception($"El jugador con dni '{jugadorExiste.Dni}' ya existe");
            });
            _tennisContext.AddRange(jugadores.ToJugadores());
            int response = await _tennisContext.SaveChangesAsync();
            return response > 0;

        }
        public async Task<Jugador> EditJugador(Jugador jugador)
        {
            Jugador? jugadorExiste = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni && e.Activo == true).FirstOrDefaultAsync();
            if (jugadorExiste == null)
                throw new Exception($"El jugador con dni '{jugadorExiste.Dni}' no existe");
            _tennisContext.Update(jugador);
            await _tennisContext.SaveChangesAsync();
            Jugador? response = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == jugador.Dni).FirstOrDefaultAsync();
            return response;
        }
        public async Task<bool> Deleted(int dni)
        {
            Jugador? jugador = await _tennisContext.Set<Jugador>().Where((e) => e.Dni == dni).FirstOrDefaultAsync();
            if (jugador == null)
                throw new Exception($"El jugador con dni '{jugador.Dni}' no existe");
            jugador.Activo = false;
            _tennisContext.Update(jugador);
            int result = await _tennisContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
