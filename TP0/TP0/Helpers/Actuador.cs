using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Actuador
    {
        public List<Regla> Reglas;
        public void verificarRegla()
        {
            if (Reglas.All(c=>c.seCumple))
            {
                ejecutarRegla();
            }
        }
        public void ejecutarRegla()
        {
            //Metodo para ejecutar la regla
        }
    }
}