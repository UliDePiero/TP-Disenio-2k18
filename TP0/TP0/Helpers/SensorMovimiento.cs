using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class SensorMovimiento : Sensor
    {
        public SensorMovimiento(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorMovimiento()
        {
        }

        public override void Medir(int valorMedicion)
        {
            Medicion.Medida = valorMedicion;
        }
    }
}