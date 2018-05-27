using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class DispositivoEstandar
    {
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public float kWxHora;
        [JsonProperty]
        public int horasXDia;

        public void convertirEnInteligente()
        {
            DispositivoInteligente convertido = new DispositivoInteligente(this);
        }
        public float consumo()
        {
            return kWxHora*horasXDia;
        }
    }
}