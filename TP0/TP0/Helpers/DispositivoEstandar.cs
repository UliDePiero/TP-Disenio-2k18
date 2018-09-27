using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class DispositivoEstandar : Dispositivo
    {
        [JsonProperty]
        public double HorasXDia;
        public double Min;
        public double Max;

        /*public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia)
        {
            id = idnuevo;
            nombre = nom;
            kWxHora = kWxH;
            horasXDia = hxdia;
        }*/

        public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia, double mx, double mn)
        {
            Codigo = idnuevo;
            Nombre = nom;
            KWxHora = kWxH;
            HorasXDia = hxdia;
            Max = mx;
            Min = mn;
            EsInteligente = false;
        }


        public DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            DispositivoInteligente convertido = null;
            switch (marca)
            {
                
                case "Samsung":
                    //AdaptadorSamsug convertido = new AdaptadorSamsung(...)
                    convertido = new AdaptadorSamsung(Nombre, Codigo, KWxHora,Max,Min);
                    break;
                case "HP":
                    convertido = new AdaptadorHp(Nombre, Codigo, KWxHora,Max,Min);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple(Nombre, Codigo, KWxHora,Max,Min);
                    break;
            }

            return convertido;
        }

        public double Consumo()
        {
            return HorasXDia * KWxHora;
        }
        public double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fFinal.Subtract(fInicial).Days*Consumo();
        }
    }
}