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
        public string id;
        [JsonProperty]
        public float kWxHora;
        [JsonProperty]
        public int horasXDia;

        public DispositivoInteligente convertido;

        public void convertirEnInteligente(string tipo)
        {
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
        public float consumo()
        {
            return kWxHora*horasXDia;
        }
    }
}