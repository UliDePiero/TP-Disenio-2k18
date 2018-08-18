using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Sensor
    {
        public float medicion;
        public List<Condicion> observers;
        public void agregarObservador(Condicion c)
        {
            observers.Add(c);
        }
        public void quitarObservador(Condicion c)
        {
            observers.Remove(c);
        }
        public void notificar()
        {
            observers.ForEach(o => o.Notificar(medicion));
        }
    }
}