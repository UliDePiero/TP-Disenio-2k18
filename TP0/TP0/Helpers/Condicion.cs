using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Condicion
    {
        Regla actuador;

        public bool seCumple;
        public float valorMax;
        public float valorMin;

        public void Notificar(float valor)
        {
            seCumple = false;
            if (valor >= valorMin && valor <= valorMax)
            {
                seCumple = true;
            }

            actuador.verificarRegla();
        }
        public bool ChequearComportamiento()
        {
            return seCumple;
        }
    }
}