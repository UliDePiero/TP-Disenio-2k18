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
        public List<Evento> eventos;

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
            
            double hs = Estado.consumoEnHoras(horas);
            return hs * kWxHora;
        }

/*        public float consumoEnPeriodo(DateTime finicial, DateTime ffinal)
        {
            double hs = Estado.HorasEncendidoEnPeriodo(finicial, ffinal);
            return hs * kWxHora;
        }
        */

        public List<Evento> filtrarLista(float horas)
        {
           return eventos.Where(x => x.horasDiferencia() <= horas).ToList();
        }

        public State siguienteEstado(int n)
        {
            return eventos.ElementAt(n - 1).TipoEvento;
        }

        public DateTime fechaEvento(int n)
        {
            return eventos.ElementAt(n).Tiempo;
        }

        
 

        public void agregarEvento(State Estado)
        {
            Evento e = new Evento() { TipoEvento = Estado, Tiempo = new DateTime() };
            eventos.Add(e);
        }

    }
}
