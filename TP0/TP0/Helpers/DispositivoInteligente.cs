using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class DispositivoInteligente
    {
        public string id;
        public string nombre;
        public bool encendido;
        public bool ahorroDeEnergia;
        public List<Evento> eventos;
        //hacer constructor con dispostivo estandar
        public DispositivoInteligente(string nom, string idnuevo)
        {
            nombre = nom;
            id = idnuevo;
        }
        public bool estaEncendido()
        {
            return encendido;
        }
        public bool estaApagado()
        {
            return !encendido;
        }
        public void encender()
        {
            if (encendido != true){
                    Evento e = new Evento();
                    encendido = true;
                    ahorroDeEnergia = false;
                    eventos.Add(e);
            }
        }
        public void apagar()
        {
            if (encendido == true){                   
                    encendido = false;
                    ahorroDeEnergia = false;                    
            }
        }
        public void ahorrarEnergia()
        {
            ahorroDeEnergia = true;
        }
        public float consumoenhoras(float horas)
        {
            float consumo = 0;
            return consumo;
        }
        public float consumoEnPeriodo(DateTime finicial, DateTime ffinal)
        {
            float consumo = 0;
            return consumo;
        }
    }
}
