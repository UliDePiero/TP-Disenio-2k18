using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class DispositivoEstandar
    {
        FechasAdmin fadmin;
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
        public float consumoEnPeriodo(DateTime finicial, DateTime ffinal)
        {
            int meses = fadmin.diferenciaDeMeses(finicial, ffinal);
            return kWxHora*horasXDia*meses;
        }
    }
}