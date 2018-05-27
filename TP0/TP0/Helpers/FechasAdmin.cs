using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class FechasAdmin
    {
        DateTime fechaActual = DateTime.Now;

        public int diferenciaDeMeses(DateTime fechaDeInicial, DateTime fechaFinal)
        {
            int mesesTotales = 0;
            int dias = fechaActual.Day - fechaFinal.Day;
            int meses = fechaActual.Month - fechaFinal.Month;
            int años = fechaActual.Year - fechaFinal.Year;
            if (dias < 0)
            {
                meses--;
            }
            if (meses > 0)
            {
                mesesTotales += meses;
            }
            else
            {
                mesesTotales -= meses;
            }
            if (años > 0)
            {
                mesesTotales += años * 12;
            }
            return mesesTotales;
        }
    }
}