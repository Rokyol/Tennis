using Tennis.API.Models.Request;
using Tennis.Models;

namespace Tennis.Mappers.Usuarios
{
    public static class UsuarioExtensions
    {
        public static Usuario ToUsuario(UsuarioRequest usuarioRequest)
        {
            return new Usuario
            {
                Username = usuarioRequest.Username,
                Password = usuarioRequest.Password
            };
        }
    }
}