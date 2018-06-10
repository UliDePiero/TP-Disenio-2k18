using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Comportamiento
    {
        public Regla regla;
        public void Notificar(float valor)
        {
            if (regla.ChequearComportamiento())
            {
                //Metodo para notificar
            }
        }
    }
}