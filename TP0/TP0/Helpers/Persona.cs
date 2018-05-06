using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Persona
    {
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public string apellido;
        [JsonProperty]
        public string domicilio;
        [JsonIgnore]
        public DateTime fechaDeAlta = new DateTime();
        [JsonProperty]
        public string usuario;
        [JsonProperty]
        public string contraseña;
    }
}