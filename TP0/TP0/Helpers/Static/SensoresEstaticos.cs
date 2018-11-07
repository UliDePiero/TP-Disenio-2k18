using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class SensoresEstaticos
    {
        static List<Sensor> sensors = new List<Sensor>
        {
            new Sensor("Humedad",1),
            new Sensor("Luz",1),
            new Sensor("Temperatura",1),
            new Sensor("Movimiento",1)
        };

        public static List<Sensor> GetSensores()
        {
            return sensors;
        }
    }
}