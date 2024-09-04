﻿using System.ComponentModel.DataAnnotations;

namespace Seguros_ABC.Models
{
    public class Asegurado
    {
        [Key]
        public required int NumeroIdentificacion { get; set; }


        public required string PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }


        public required string PrimerApellido { get; set; }


        public required string SegundoApellido { get; set; }


        [Phone]
        public required string Telefono { get; set; }


        [EmailAddress]
        public required string Email { get; set; }


        public required DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Currency)]
        public required decimal ValorEstimadoSeguro { get; set; }

        public string? Observaciones { get; set; }
    }
}

    

