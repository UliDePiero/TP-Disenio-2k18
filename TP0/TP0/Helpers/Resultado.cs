using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Resultado
    {
        string dispositivo;
        double horasMaxConsumo;

        public Resultado()
        {
        }

        public Resultado(string nombre, double horas)
        {
            dispositivo = nombre;
            horasMaxConsumo = horas;
        }

    }
}