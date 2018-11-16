using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Reporte
    {
        public string tipoReporte;
        public string id;
        public double consumo;
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public Reporte() { }
        public Reporte(string _tipo, string _id, double _consumo, DateTime _fechaInicio, DateTime _fechaFin)
        {
            id = _id;
            tipoReporte = _tipo;
            consumo = _consumo;
            fechaInicio = _fechaInicio;
            fechaFin = _fechaFin;
        }
    }
}