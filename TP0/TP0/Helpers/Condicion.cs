using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Condicion
    {
#pragma warning disable CS0649 // Field 'Condicion.actuador' is never assigned to, and will always have its default value null
        Regla actuador;
#pragma warning restore CS0649 // Field 'Condicion.actuador' is never assigned to, and will always have its default value null

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