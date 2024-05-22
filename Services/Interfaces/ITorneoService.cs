using Microsoft.AspNetCore.Mvc;
using Tennis.Models;
using Tennis.Models.Response;

namespace Tennis.Services.Interfaces
{
    public interface ITorneoService
    {
        Task<bool> CreateTorneo(Torneo torneo, int userId);
        Task<Torneo> GetTorneo(int id);
        Task<Torneo> GetTorneoByNombre(string nombre);
        Task<TorneoTerminadoResponse> IniciarTorneo(Torneo torneo);
        Task<List<Torneo>> GetTorneosByFecha(DateTime fechaDesde, DateTime fechaHasta);
    }
}
