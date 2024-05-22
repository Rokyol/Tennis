﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Tennis.API.Middlewares.CustomDataAnnotations
{
    public class ValidNameAttribute : ValidationAttribute
    {
        private readonly string _comienzaCon;

        public ValidNameAttribute(string comienzaCon)
        {
            _comienzaCon = comienzaCon;
        }

        protected override ValidationResult IsValid(object texto, ValidationContext validationContext)
        {
            if (texto == null || string.IsNullOrWhiteSpace(texto.ToString()))
            {
                return new ValidationResult("El campo nombre no debe estar vacio.");
            }

            string nombre = texto.ToString();

            if (!nombre.StartsWith(_comienzaCon))
            {
                return new ValidationResult($"El nombre debe comenzar con: '{_comienzaCon}'.");
            }

            return ValidationResult.Success;
        }
    }
}
