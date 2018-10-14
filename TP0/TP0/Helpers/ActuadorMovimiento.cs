using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class ActuadorMovimiento : Actuador
    {
        public ActuadorMovimiento(int dispositivoID)
        {
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }
    }
}