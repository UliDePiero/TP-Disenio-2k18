using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class FechasAdmin
    {
        DateTime fechaActual = DateTime.Now;

        public int DiferenciaDeMeses(DateTime fechaDeAlta)
        {
            int mesesTotales = 0;
            int dias = fechaActual.Day - fechaDeAlta.Day;
            int meses = fechaActual.Month - fechaDeAlta.Month;
            int años = fechaActual.Year - fechaDeAlta.Year;
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