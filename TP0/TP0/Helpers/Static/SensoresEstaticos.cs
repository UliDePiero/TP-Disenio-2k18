using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public static IEnumerable<SelectListItem> GetTipos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (Sensor d in sensors)
                lista.Add(new SelectListItem() { Value = d.Desc, Text = d.Desc });
            return lista;
        }
        public static IEnumerable<SelectListItem> GetDescripcion()
        {
            return new List<SelectListItem>() { new SelectListItem() { Value = "apagar", Text = "apagar" }, new SelectListItem() { Value = "encender", Text = "encender" }, new SelectListItem() { Value = "ahorro", Text = "ahorro" } };
        }
    }
}