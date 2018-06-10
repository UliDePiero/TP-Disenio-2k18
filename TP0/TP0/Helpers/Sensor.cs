using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Sensor
    {
        public float valor;
        public List<Regla> observers;
        public void agregarObservador(Regla c)
        {
            observers.Add(c);
        }
        public void quitarObservador(Regla c)
        {
            observers.Remove(c);
        }
        public void notificar()
        {
            observers.ForEach(o => o.notificar(valor));
        }
        public void censar()
        {

        }
    }

    public class SensorHumedad : Sensor
    {
        new public void censar()
        {

        }
    }
    public class SensorMovimiento : Sensor
    {
        new public void censar()
        {

        }
    }
    public class SensorTemperatura : Sensor
    {
        new public void censar()
        {

        }
    }
    public class SensorLuz : Sensor
    {
        new public void censar()
        {

        }
    }
}