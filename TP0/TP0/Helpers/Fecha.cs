using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Fecha
    {
        public Fecha(int a, int m, int d)
        {
            anio = a;
            mes = m;
            dia = d;
        }
        public int anio;
        public int mes;
        public int dia;
    }
}