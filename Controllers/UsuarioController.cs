using Microsoft.AspNetCore.Mvc;
using Tennis.API.Models.Request;
using Tennis.API.Services.Interfaces;
using Tennis.Services.Interfaces;
using Tennis.API.Models.Request;
using Tennis.Models.Request;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioController(IUsuarioService usuarioService, IAuthenticationService authenticationService)
        {
            _usuarioService = usuarioService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<IActionResult> CreateUserAsync(UsuarioRequest usuarioRequest)
        {
            await _usuarioService.CreateUserAsync(usuarioRequest);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(UsuarioLoginRequest usuarioRequest)
        {
            var userValidado = await _usuarioService.ValidateUserAsync(usuarioRequest);

            var token = _authenticationService.GenerateToken(userValidado);

            await _authenticationService.UpdateRefreshToken(userValidado, token.RefreshToken);

            return Ok(token);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest refreshToken)
        {
            var userFromDb = await _usuarioService.GetUserByRefreshTokenAsync(refreshToken.RefreshToken);

            if (userFromDb == null)
                return Unauthorized($"No existe usuario con el {refreshToken.RefreshToken} ingresado");

            if (userFromDb.RefreshTokenExpiration.HasValue)
            {
                if (!_authenticationService.ValidateRefreshToken(userFromDb))
                    return Unauthorized("El token de refresco ha expirado");
            }

            var token = _authenticationService.GenerateToken(userFromDb);

            if (token == null)
                return Unauthorized("No se pudo generar el token");

            await _authenticationService.UpdateRefreshToken(userFromDb, token.RefreshToken);

            return Ok(token);
        }
    }
}
