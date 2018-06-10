using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Sensor
    {
        public float valor;
        public List<Comportamiento> observers;
        public void agregarObservador(Comportamiento c)
        {
            observers.Add(c);
        }
        public void quitarObservador(Comportamiento c)
        {
            observers.Remove(c);
        }
        public void notificar()
        {
            observers.ForEach(o => o.Notificar(valor));
        }
    }
}