using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    // UN ACTUADOR ES UN CONJUNTO DE REGLAS SIMPLES
    public class Actuador
    {
        public DispositivoInteligente DI;
        public List<Condicion> Condiciones;
        public void verificarRegla()
        {
            if (Condiciones.All(c=>c.seCumple))
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