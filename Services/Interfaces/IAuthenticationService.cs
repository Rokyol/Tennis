
using Tennis.API.Models.Response;
using Tennis.Models;

namespace Tennis.API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        TokenResponse GenerateToken(Usuario usuario);
        bool ValidateRefreshToken(Usuario usuario);
        Task UpdateRefreshToken(Usuario userFromDB, string refreshToken);
    }
}