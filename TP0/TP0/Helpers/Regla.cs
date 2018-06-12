using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    // UN ACTUADOR ES UN CONJUNTO DE REGLAS SIMPLES
    public class Regla
    {
        public List<Condicion> Reglas;
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