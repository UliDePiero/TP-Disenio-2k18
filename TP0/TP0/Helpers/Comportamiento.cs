using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Comportamiento
    {
        public Regla regla;
        public bool seCumple;
        public float valorMax;
        public float valorMin;
        public void notificar(float valor)
        {
            if (valor > valorMin && valor < valorMax)
            {
                seCumple = true;
            }
            regla.chequearComportamientos();
        }
    }
}