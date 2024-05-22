using System.ComponentModel.DataAnnotations;

namespace Tennis.Models.Request
{
    public class UsuarioLoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
