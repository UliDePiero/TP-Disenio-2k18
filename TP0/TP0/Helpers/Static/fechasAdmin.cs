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

        public static double ConsumoHsTotalPeriodo(DateTime fInicialPer, DateTime fFinalPer, List<State> estadosAntes)
        {
            double consumo = 0;
            List<State> EstadosPartePeriodo = estadosAntes.Where(x => parteDelPeriodo(fInicialPer, fFinalPer, x)).ToList();
            
            if (EstadosPartePeriodo.Count() == 1) //igual de tamaño , o mayor de tamaño
                return EstadosPartePeriodo.ElementAt(0).ConsumoEnIntervalor(fInicialPer, fFinalPer);

            foreach (State e in EstadosPartePeriodo)
            {
                if (dentroDelPeriodo(fInicialPer, fFinalPer, e))
                    consumo += e.ConsumoEnIntervalor(fInicialPer, fFinalPer);

                else
                    consumo += consumoExtremoPeriodo(fInicialPer, fFinalPer, e);
            }

            return consumo;
            
        }

        public static double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            if (Estado.FechaInicial <= fFinal && fFinal <= Estado.FechaFinal )
                return Estado.ConsumoEnIntervalor(Estado.FechaInicial, fFinal);
            else
                return Estado.ConsumoEnIntervalor(fInicial, Estado.FechaFinal);
        }

        public static bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return fInicial < Estado.FechaInicial && Estado.FechaFinal < fFinal;
        }

        public static bool parteDelPeriodo(DateTime fInicialPer, DateTime fFinalPer, State Estado)
        {
            return (fInicialPer <= Estado.FechaInicial && Estado.FechaInicial <= fFinalPer) || (fInicialPer <= Estado.FechaFinal && Estado.FechaFinal <= fFinalPer);
        }

    }

}