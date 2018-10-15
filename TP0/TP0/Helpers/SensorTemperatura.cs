using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class SensorTemperatura : Sensor
    {
        public SensorTemperatura(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorTemperatura()
        {
        }

        public override void Medir(int valorMedicion)
        {
            Medicion.Medida = valorMedicion;
        }
    }
}