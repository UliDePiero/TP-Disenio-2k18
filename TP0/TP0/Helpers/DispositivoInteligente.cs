using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TP0.Helpers
{
    public class DispositivoInteligente
    {
        FechasAdmin fadmin;
        public string id;
        public string nombre;
        public State Estado;
        public float kWxHora;
        public List<State> estadosAnteriores;

        //hacer constructor con dispostivo estandar
        public DispositivoInteligente(string nom, string idnuevo)
        {
            nombre = nom;
            id = idnuevo;
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
        public double consumoEnHoras(float horas)
        {
            DateTime fFinal = new DateTime();
            DateTime fInicial = fFinal.AddHours(-horas);

            double hs = ConsumoHsTotalPeriodo(fInicial, fFinal);
            return hs * kWxHora;
        }

       public double consumoEnPeriodo(DateTime finicial, DateTime ffinal)
       { 
       double hs = ConsumoHsTotalPeriodo(finicial, ffinal);
       return hs * kWxHora;
       }
       

        public double ConsumoHsTotalPeriodo(DateTime fInicial, DateTime fFinal)
        {
            double consumo=0;
            List<State> CambiosEstadosDentroPeriodo = estadosAnteriores.Where(x => x.parteDelPeriodo(fInicial, fFinal)).ToList();

            if (CambiosEstadosDentroPeriodo.Count() == 0)
               return Estado.consumoEnHoras(fInicial, fFinal);

            foreach (State e in CambiosEstadosDentroPeriodo)
            {
                if (e.dentroDelPeriodo(fInicial, fFinal))
                    consumo += e.consumoEnHoras(fInicial, fFinal);

                else
                    consumo = e.consumoExtremoPeriodo(fInicial, fFinal);
                
            }

            return consumo;
        }


        public void agregarEvento(State Estado)
        {
            DateTime fechaActual = new DateTime();

            if (estadosAnteriores.Count()!=0)
            estadosAnteriores.Last().FechaFinal = fechaActual;

            estadosAnteriores.Add(Estado);
        }

    }
}
