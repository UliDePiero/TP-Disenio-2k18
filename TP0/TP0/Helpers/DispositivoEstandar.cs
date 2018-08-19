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
        public double min;
        public double max;

        /*public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia)
        {
            id = idnuevo;
            nombre = nom;
            kWxHora = kWxH;
            horasXDia = hxdia;
        }*/

        public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia, double mx, double mn)
        {
            id = idnuevo;
            nombre = nom;
            kWxHora = kWxH;
            horasXDia = hxdia;
            max = mx;
            min = mn;
        }


        public DispositivoInteligente convertirEnInteligente(string marca)
        {
            DispositivoInteligente convertido = null;
            switch (marca)
            {
                
                case "Samsung":
                    //AdaptadorSamsug convertido = new AdaptadorSamsung(...)
                    convertido = new AdaptadorSamsung(nombre, id, kWxHora,max,min);
                    break;
                case "HP":
                    convertido = new AdaptadorHp(nombre, id, kWxHora,max,min);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple(nombre, id, kWxHora,max,min);
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