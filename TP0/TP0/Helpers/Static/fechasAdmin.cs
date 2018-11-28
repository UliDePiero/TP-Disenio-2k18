using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class FechasAdmin
    {


        public static int DiferenciaDeMeses(DateTime fechaDeInicial, DateTime fechaFinal)
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

        public static double HsConsumidasTotalPeriodo(DateTime fInicialPer, DateTime fFinalPer, ICollection<State> estadosAntes)
        {
            double Hs = 0;
            foreach (State e in estadosAntes)
            {
                if (e.FechaFinal == new DateTime(3000, 1, 1)) //Si el estado no termino, se usa la fecha de ahora como la final
                    e.FechaFinal = DateTime.Now;

                if (DentroDelPeriodo(fInicialPer, fFinalPer, e))
                    Hs += e.CalculoHoras(e.FechaInicial, e.FechaFinal);
                else if (ParteDelPeriodoIzq(fInicialPer, fFinalPer, e))
                    Hs += e.CalculoHoras(e.FechaInicial, fFinalPer);
                else if (ParteDelPeriodoDer(fInicialPer, fFinalPer, e))
                    Hs += e.CalculoHoras(fInicialPer, e.FechaFinal);
            }
            return Hs;
        }

        public static double ConsumoExtremoPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            if (Estado.FechaInicial <= fFinal && fFinal <= Estado.FechaFinal)
                return Estado.CalculoHoras(Estado.FechaInicial, fFinal);
            else
                return Estado.CalculoHoras(fInicial, Estado.FechaFinal);
        }

        public static bool DentroDelPeriodo(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return  (fInicial <= Estado.FechaInicial && Estado.FechaFinal > fInicial && Estado.FechaFinal <= fFinal && fFinal > Estado.FechaInicial);
        }

        public static bool ParteDelPeriodoIzq(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return (fInicial <= Estado.FechaInicial && fFinal <= Estado.FechaFinal && Estado.FechaInicial < fFinal);
        }

        public static bool ParteDelPeriodoDer(DateTime fInicial, DateTime fFinal, State Estado)
        {
            return (Estado.FechaInicial <= fInicial && Estado.FechaFinal <= fFinal && fInicial < Estado.FechaFinal);
        }
    }

}