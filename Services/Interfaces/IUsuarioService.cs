using Tennis.API.Models.Request;
using Tennis.Models;
using Tennis.Models.Request;

namespace Tennis.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> ValidateUserAsync(UsuarioLoginRequest usuarioRequest);
        Task CreateUserAsync(UsuarioRequest usuarioRequest);
        Task<Usuario> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
