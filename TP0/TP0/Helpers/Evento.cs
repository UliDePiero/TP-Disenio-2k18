using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Evento
    {
        public State TipoEvento;
        public DateTime Tiempo;

        public double horasDiferencia()
        {

            DateTime date = new DateTime();
            return (date - Tiempo).TotalHours;
        }
    }

   

}