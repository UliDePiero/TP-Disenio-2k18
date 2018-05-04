using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class FechasAdmin
    {
        DateTime fechaActual = DateTime.Now;

        public int diferenciaDeMeses(DateTime fechaDeAlta)
        {
            int mesesTotales = 0;
            int dias;
            int meses;
            int años;
            dias = fechaActual.Day - fechaDeAlta.Day;
            meses = fechaActual.Month - fechaDeAlta.Month;
            años = fechaActual.Year - fechaDeAlta.Year;
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