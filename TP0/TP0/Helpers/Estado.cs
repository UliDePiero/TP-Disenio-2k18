using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class State
    {
        public DateTime FechaInicial;
        public DateTime FechaFinal;

        public abstract void Encender(DispositivoInteligente dips);

        public abstract void Apagar(DispositivoInteligente dips);

        public abstract void AhorrarEnergia(DispositivoInteligente dips);

        public abstract double consumoEnIntervalor(DateTime fInicial, DateTime fFinal);

        public abstract double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal);

        public abstract bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal);

        public abstract bool parteDelPeriodo(DateTime fInicial, DateTime fFinal);

    }

        class Encendido : State
    {
        // Constructor
        public Encendido()
        {
            FechaInicial = new DateTime();
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }
        public override void Encender(DispositivoInteligente dips) => throw new NotImplementedException();

        public override void Apagar(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Apagado());
        }

        public override void AhorrarEnergia(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Ahorro());
        }

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours;
            return diff;
        }

        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
               return Static.FechasAdmin.consumoExtremoPeriodo(fInicial, fFinal, this);
        }

        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
                return Static.FechasAdmin.dentroDelPeriodo(fInicial, fFinal, this);
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
                return Static.FechasAdmin.parteDelPeriodo(fInicial, fFinal, this);
        }
            
     }

    class Apagado : State
    {
        // Constructor
        public Apagado()
        {
            FechaInicial = new DateTime();
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
            }

        public override void Encender(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Encendido());
            }

        public override void Apagar(DispositivoInteligente dips) => throw new NotImplementedException();

        public override void AhorrarEnergia(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Ahorro());
            }

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
           return 0;
        }
        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.consumoExtremoPeriodo(fInicial, fFinal, this);
        }

        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.dentroDelPeriodo(fInicial, fFinal, this);
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.parteDelPeriodo(fInicial, fFinal, this);
        }

    }

    class Ahorro : State
    {
        // Constructor
        public Ahorro()
        {
            FechaInicial = new DateTime();
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public override void Encender(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Encendido());
        }

        public override void Apagar(DispositivoInteligente dips)
        {
            FechaFinal = new DateTime();
            dips.agregarEstado(new Apagado());
        }

        public override void AhorrarEnergia(DispositivoInteligente dips) => throw new NotImplementedException();

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/3;
            return diff;
        }

        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.consumoExtremoPeriodo(fInicial, fFinal, this);
        }

        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.dentroDelPeriodo(fInicial, fFinal, this);
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
           return Static.FechasAdmin.parteDelPeriodo(fInicial, fFinal, this);
         }

    }
}