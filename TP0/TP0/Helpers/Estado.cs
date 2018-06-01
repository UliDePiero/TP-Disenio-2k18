using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class State
    {
        protected DispositivoInteligente DI;

        public DateTime FechaInicial;
        public DateTime FechaFinal;

        //Propiedades
        public DispositivoInteligente Di
        {
            get { return DI; }
            set { DI = value; }
        }
        public DateTime fechaInicialEstado
        {
            get { return FechaInicial; }
            set { FechaInicial = value; }
        }
        public DateTime fechaFinalEstado
        {
            get { return FechaFinal; }
            set { FechaFinal = value; }
        }


        public abstract void Encender();
        public abstract void Apagar();
        public abstract void AhorrarEnergia();
        public abstract double consumoEnHoras(DateTime fInicial, DateTime fFinal);
        public abstract bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal);
        public abstract bool parteDelPeriodo(DateTime fInicial, DateTime fFinal);
        public abstract double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal);


    }

    class Encendido : State
    {
        // Constructor
        public Encendido(State state)
        {
            DI = state.Di;
            fechaInicialEstado = new DateTime();
            fechaFinalEstado = new DateTime(3000, 1, 1, 0, 0, 0);
            Di.agregarEvento(state);
        }

        public override void Encender() => throw new NotImplementedException();

        public override void Apagar()
        {
            Di.Estado = new Apagado(this);
        }

        public override void AhorrarEnergia()
        {
            Di.Estado = new Ahorro(this);
        }
        public override double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
                double diff = (fFinal - fInicial).TotalHours;
                return diff;
            
        }

        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (fechaInicialEstado <= fFinal)
                return consumoEnHoras(fechaInicialEstado, fFinal);
            else
                return consumoEnHoras(fInicial, fechaFinalEstado);
        }
        

        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fInicial < fechaInicialEstado && fechaFinalEstado < fFinal ;
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return (fInicial <= fechaInicialEstado && fechaInicialEstado <= fFinal) || (fInicial <= fechaFinalEstado && fechaFinalEstado <= fFinal);
        }

    }

    class Apagado : State
    {
        // Constructor
        public Apagado(State state)
        {
            DI = state.Di;
            fechaInicialEstado = new DateTime();
            fechaFinalEstado = new DateTime(3000, 1, 1, 0, 0, 0);
            Di.agregarEvento(state);
        }

        public override void Encender()
        {
            DI.Estado = new Encendido(this);
        }

        public override void Apagar() => throw new NotImplementedException();

        public override void AhorrarEnergia()
        {
            DI.Estado = new Ahorro(this);
        }

        public override double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
           return 0;
        }
        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (fechaInicialEstado <= fFinal)
                return consumoEnHoras(fechaInicialEstado, fFinal);
            else
                return consumoEnHoras(fInicial, fechaFinalEstado);
        }


        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fInicial < fechaInicialEstado && fechaFinalEstado < fFinal;
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return (fInicial <= fechaInicialEstado && fechaInicialEstado <= fFinal) || (fInicial <= fechaFinalEstado && fechaFinalEstado <= fFinal);
        }
    }

    class Ahorro : State
    {
        // Constructor
        public Ahorro(State state)
        {
            DI = state.Di;
            fechaInicialEstado = new DateTime();
            fechaFinalEstado = new DateTime(3000, 1, 1, 0, 0, 0);
            Di.agregarEvento(state);
        }

        public override void Encender()
        {
            DI.Estado = new Encendido(this);
        }

        public override void Apagar()
        {
            DI.Estado = new Apagado(this);
        }

        public override void AhorrarEnergia() => throw new NotImplementedException();


        public override double consumoEnHoras(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/3;
            return diff;
        }
        public override double consumoExtremoPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (fechaInicialEstado <= fFinal)
                return consumoEnHoras(fechaInicialEstado, fFinal);
            else
                return consumoEnHoras(fInicial, fechaFinalEstado);
        }


        public override bool dentroDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fInicial < fechaInicialEstado && fechaFinalEstado < fFinal;
        }

        public override bool parteDelPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return (fInicial <= fechaInicialEstado && fechaInicialEstado <= fFinal) || (fInicial <= fechaFinalEstado && fechaFinalEstado <= fFinal);
        }


    }


}