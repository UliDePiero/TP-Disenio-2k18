using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class SensorHumedad : Sensor
    {
        public SensorHumedad(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorHumedad()
        {
        }

        public override void Medir(int valorMedicion)
        {
            Medicion.Medida = valorMedicion;
        }
    }
}