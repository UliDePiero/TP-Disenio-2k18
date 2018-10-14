using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class SensorLuz : Sensor
    {
        public SensorLuz(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorLuz()
        {
        }

        public override void Medir(int valorMedicion)
        {
            Medicion.Medida = valorMedicion;
        }
    }
}