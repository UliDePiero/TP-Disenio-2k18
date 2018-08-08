using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class FechasAdmin
    {


        public static int diferenciaDeMeses(DateTime fechaDeInicial, DateTime fechaFinal)
        {
            DateTime fechaActual = DateTime.Now;

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

        public static double ConsumoHsTotalPeriodo(DateTime fInicial, DateTime fFinal, List<State> estadosAntes)
        {
            double consumo = 0;
            List<State> CambiosEstadosDentroPeriodo = estadosAntes.Where(x => x.parteDelPeriodo(fInicial, fFinal)).ToList();

            if (CambiosEstadosDentroPeriodo.Count() == 1)
                return CambiosEstadosDentroPeriodo.ElementAt(0).consumoEnIntervalor(fInicial, fFinal);

            foreach (State e in CambiosEstadosDentroPeriodo)
            {
                if (e.dentroDelPeriodo(fInicial, fFinal))
                    consumo += e.consumoEnIntervalor(fInicial, fFinal);

                else
                    consumo = e.consumoExtremoPeriodo(fInicial, fFinal);

            }

            return consumo;
        }

        public static double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            if (Estado.FechaInicial <= fFinal)
                return Estado.consumoEnIntervalor(Estado.FechaInicial, fFinal);
            else
                return Estado.consumoEnIntervalor(fInicial, Estado.FechaFinal);
        }

        public static bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return fInicial < Estado.FechaInicial && Estado.FechaFinal < fFinal;
        }

        public static bool parteDelPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return (fInicial <= Estado.FechaInicial && Estado.FechaInicial <= fFinal) || (fInicial <= Estado.FechaFinal && Estado.FechaFinal <= fFinal);
        }

    }

}