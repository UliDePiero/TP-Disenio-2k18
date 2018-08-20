using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TP0.Helpers
{
    public class DispositivoInteligente
    {
        [JsonProperty]
        public string id;
        [JsonProperty]
        public string nombre;
        public State Estado;
        [JsonProperty]
        public double kWxHora;
        [JsonProperty]
        public List<State> estadosAnteriores;
        public double min;
        public double max;


        /*public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
        }*/
        
        public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
            max = mx;
            min = mn;
            estadosAnteriores = new List<State>();
            agregarEstado(new Apagado(this));
    }


        public bool estaEncendido()
        {
            return Estado is Encendido;
        }
        public bool estaApagado()
        {
            return Estado is Apagado ;
        }
        public void encender()
        {
            Estado.Encender();
        }
        public void apagar()
        {
            Estado.Apagar();
        }
        public void ahorrarEnergia()
        {
            Estado.AhorrarEnergia();
        }
        public double consumoEnHoras(double horas)
        {
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return hs * kWxHora;
        }

       public double consumoEnPeriodo(DateTime fInicial, DateTime fFinal)
       { 
           double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
           return hs * kWxHora;
       }

       public void agregarEstado(State est)
       {
           Estado = est;
           estadosAnteriores.Add(est);
       }

    }
}
