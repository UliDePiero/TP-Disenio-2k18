using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Documento
    {
        [JsonProperty]
        public string tipo;
        [JsonProperty]
        public string numero;
    }
}