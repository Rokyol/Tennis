using Microsoft.EntityFrameworkCore;
using Tennis.API.Models.Request;
using Tennis.API.Services.Encryption;
using Tennis.Models;
using Tennis.Models.Request;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TennisContext _tennisContext;

        private readonly IEncryptionService _encryptionService;

        public UsuarioService(TennisContext tennisContext, IEncryptionService encryptionService)
        {
            _tennisContext = tennisContext;
            _encryptionService = encryptionService;
        }
        public async Task CreateUserAsync(UsuarioRequest usuarioRequest)
        {
            var usuario = await _tennisContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Username.Equals(usuarioRequest.Username));

            if (usuario != null)
                throw new Exception($"El usuario {usuarioRequest.Username} ya existe");

            Usuario newUsuario = new Usuario();
            newUsuario.Username = usuarioRequest.Username;
            newUsuario.Password = usuarioRequest.Password;
            newUsuario.Rol= usuarioRequest.Rol;
            newUsuario.Password = _encryptionService.Encrypt(usuarioRequest.Password);

            _tennisContext.Add(newUsuario);

            await _tennisContext.SaveChangesAsync();
        }

        public async Task<Usuario> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = await _tennisContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken));

            return user;
        }

        public async Task<Usuario> ValidateUserAsync(UsuarioLoginRequest usuarioRequest)
        {
            var passEncypted = _encryptionService.Encrypt(usuarioRequest.Password);

            var user = await _tennisContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Username.Equals(usuarioRequest.Username) &&
                u.Password.Equals(passEncypted));

            if (user == null)
                throw new Exception("Credenciales no validas");

            return user;
        }
    }
}
