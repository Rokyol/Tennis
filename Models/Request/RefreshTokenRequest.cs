﻿using System.ComponentModel.DataAnnotations;

namespace Tennis.API.Models.Request
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
