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
        [JsonProperty]
        public string nombre;
        [JsonProperty]
        public double kWxHora;
        [JsonProperty]
        public double horasXDia;
        public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia)
        {
            id = idnuevo;
            nombre = nom;
            kWxHora = kWxH;
            horasXDia = hxdia;
        }
        public DispositivoInteligente convertirEnInteligente(string tipo)
        {
            DispositivoInteligente convertido = null;
            switch (tipo)
            {
                
                case "Samsung":
                    //AdaptadorSamsug convertido = new AdaptadorSamsung(...)
                    convertido = new AdaptadorSamsung(this.nombre, this.id, this.kWxHora);
                    break;
                case "HP":
                    convertido = new AdaptadorHp(this.nombre, this.id, this.kWxHora);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple(this.nombre, this.id, this.kWxHora);
                    break;
            }

            return convertido;
        }

        public double consumo()
        {
            return horasXDia * kWxHora;
        }
        public double consumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fFinal.Subtract(fInicial).Days*consumo();
        }
    }
}