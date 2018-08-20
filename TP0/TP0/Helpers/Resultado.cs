using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Resultado
    {
        string dispositivo;
        string horasMaxConsumo;

        public Resultado()
        {
        }

        public Resultado(string nombre, string horas)
        {
            dispositivo = nombre;
            horasMaxConsumo = horas;
        }

    }
}