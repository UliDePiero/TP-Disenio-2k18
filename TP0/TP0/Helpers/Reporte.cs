using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Reporte
    {
        public string tipoReporte;
        public double consumo;
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public Reporte() { }
        public Reporte(string _tipo, double _consumo, DateTime _fechaInicio, DateTime _fechaFin)
        {
            tipoReporte = _tipo;
            consumo = _consumo;
            fechaInicio = _fechaInicio;
            fechaFin = _fechaFin;
        }
    }
}