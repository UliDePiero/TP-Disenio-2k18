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
            new Sensor("Humedad"),
            new Sensor("Luz"),
            new Sensor("Temperatura"),
            new Sensor("Movimiento")
        };

        public static List<Sensor> GetSensores()
        {
            return sensors;
        }
    }
}