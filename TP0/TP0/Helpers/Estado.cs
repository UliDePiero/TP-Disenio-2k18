using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class State
    {
        public DispositivoInteligente DI;
        public DateTime FechaInicial;
        public DateTime FechaFinal;

        public void Encender() => throw new NotImplementedException();

        public void Apagar()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Apagado(this);
        }

        public void AhorrarEnergia()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Ahorro(this);
        }
        
        public double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours;
            return diff;
        }
        public double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (FechaInicial <= fFinal)
                return consumoEnHoras(FechaInicial, fFinal);
            else
                return consumoEnHoras(fInicial, FechaFinal);
        }
        public bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fInicial < FechaInicial && FechaFinal < fFinal;
        }

        public bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return (fInicial <= FechaInicial && FechaInicial <= fFinal) || (fInicial <= FechaFinal && FechaFinal <= fFinal);
        }
    }

    class Encendido : State
    {
        // Constructor
        public Encendido(State state)
        {
            DI = state.DI;
            FechaInicial = state.FechaFinal;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
            DI.agregarEvento(state);
        }
     }

    class Apagado : State
    {
        // Constructor
        public Apagado(State state)
        {
            DI = state.DI;
            FechaInicial = state.FechaFinal;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
            DI.agregarEvento(state);
        }

        public new void Encender()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Encendido(this);
        }

        public new void Apagar() => throw new NotImplementedException();

        public new void AhorrarEnergia()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Ahorro(this);
        }

        public new double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
           return 0;
        }
    }

    class Ahorro : State
    {
        // Constructor
        public Ahorro(State state)
        {
            DI = state.DI;
            FechaInicial = state.FechaFinal;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
            DI.agregarEvento(state);
        }

        public new void Encender()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Encendido(this);
        }

        public new void Apagar()
        {
            FechaFinal = new DateTime();
            DI.Estado = new Apagado(this);
        }

        public new void AhorrarEnergia() => throw new NotImplementedException();

        public new double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/3;
            return diff;
        }
    }
}