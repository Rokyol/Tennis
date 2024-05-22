﻿using System.ComponentModel.DataAnnotations;

namespace Tennis.API.Models.Request
{
    public class UsuarioRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
