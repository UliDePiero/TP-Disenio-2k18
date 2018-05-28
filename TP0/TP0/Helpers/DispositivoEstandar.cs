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
        public string id;
        FechasAdmin fadmin;
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public float kWxHora;
        [JsonProperty]
        public int horasXDia;

        public void convertirEnInteligente(string tipo)
        {
            DispositivoInteligente convertido;
            switch (tipo)
            {
                case "Samsung":
                    convertido = new AdaptadorSamsung(this.nombre, this.id);
                    break;
                case "HP":
                    convertido = new AdaptadorHp(this.nombre, this.id);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple(this.nombre, this.id);
                    break;

            }
        }
        public float consumoEnPeriodo(DateTime finicial, DateTime ffinal)
        {
            int meses = fadmin.diferenciaDeMeses(finicial, ffinal);
            return kWxHora*horasXDia*meses;
        }
    }
}