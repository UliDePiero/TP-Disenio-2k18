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
        public DateTime fechaDeAlta;
        [JsonProperty]
        public string usuario;
        [JsonProperty]
        public string contrasenia;
        private Usuario u;

        public Usuario(Usuario u)
        {
            this.u = u;
        }

        public Usuario (string nom, string ape, string dom, DateTime dt, string usu, string contr)
        {
            this.nombre = nom;
            this.apellido = ape;
            this.domicilio = dom;
            this.fechaDeAlta = DateTime.Today;
            this.usuario = usu;
            this.contrasenia = contr;

          
        }

    }
}