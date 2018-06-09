using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Regla
    {
        public List<Comportamiento> comportamientos;
        public void chequearComportamientos()
        {
            if (comportamientos.All(c=>c.seCumple))
            {
                ejecutarRegla();
            }
        }
        public void ejecutarRegla()
        {
            //faltan actuadores
        }
    }
}