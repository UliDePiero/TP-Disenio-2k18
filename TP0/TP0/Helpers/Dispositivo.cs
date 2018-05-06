using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Dispositivo
    {
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public int kWxHora;
        [JsonProperty]
        public bool encendido;

        public bool EstaEncendido()
        {
            return encendido;
        }
        public int KWxHora()
        {
            return kWxHora;
        }
    }
}