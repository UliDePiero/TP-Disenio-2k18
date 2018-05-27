using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Usuario
    {
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public string apellido;
        [JsonProperty]
        public string domicilio;
        [JsonIgnore]
        public string fechaDeAlta;
        [JsonProperty]
        public string usuario;
        [JsonProperty]
        public string contrasenia;

    }
}