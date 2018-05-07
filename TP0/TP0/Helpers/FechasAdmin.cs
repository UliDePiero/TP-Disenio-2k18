using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class FechasAdmin
    {
        Fecha fechaActual = new Fecha(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        public int HaceCuantosMeses(Fecha fechaDeAlta)
        {
            int mesesTotales = 0;
            int dias = fechaActual.dia - fechaDeAlta.dia;
            int meses = fechaActual.mes - fechaDeAlta.mes;
            int años = fechaActual.anio - fechaDeAlta.anio;
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