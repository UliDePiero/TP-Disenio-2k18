using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;

namespace TP0.Helpers
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string FechaDeAlta { get; set; }
        public string Username { get; set; }
        public string Contrasenia { get; set; }
        public bool EsAdmin { get; set; }

    }
}