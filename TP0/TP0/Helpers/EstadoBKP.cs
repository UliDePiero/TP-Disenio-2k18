/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class State
    {
        protected DispositivoInteligente DI;

        //Propiedades
        public DispositivoInteligente Di
        {
            get { return DI; }
            set { DI = value; }
        }


        public abstract void Encender();
        public abstract void Apagar();
        public abstract void AhorrarEnergia();
        public abstract double consumoEnHoras(float horas);
        public abstract double consumoEnPeriodo(DateTime fInicial, DateTime fFinal);
        public abstract double Resta(DateTime fInicial, double acum , int cont);
    }

    class Encendido : State
    {
        // Constructor
        public Encendido(State state)
        {
            DI = state.Di;
            Di.agregarEvento(state);
        }

        public override void Encender() => throw new NotImplementedException();

        public override void Apagar()
        {
            DI.Estado = new Apagado(this);
        }

        public override void AhorrarEnergia()
        {
            DI.Estado = new Ahorro(this);
        }
        public override double consumoEnHoras(float horas)
        {
            List<Evento> eventosQueCumplenHoras = Di.filtrarLista(horas);
            double acum = 0;
            int n = eventosQueCumplenHoras.Count()-1;
            DateTime dateIn = new DateTime();


            if (n >= 0)
            {
               return Resta(dateIn, acum, n);
            }
            else
            {   
                return horas;
            }
        }

        public override float consumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {

        }

        public override double Resta(DateTime dateIn, double acum, int n)
        {
            if (n >= 0)
            {
                State SigEstado = Di.siguienteEstado(n);
                DateTime dateF = Di.fechaEvento(n);
                TimeSpan time = (dateIn - dateF);
                acum += (time.TotalHours);

                return SigEstado.Resta(dateF, acum, n - 1);
            }
            else
            {
                return acum;
            }
        }




    }

    class Apagado : State
    {
        // Constructor
        public Apagado(State state)
        {
            DI = state.Di;
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

        public override double consumoEnHoras(float horas)
        {
            List<Evento> eventosQueCumplenHoras = Di.filtrarLista(horas);
            double acum = 0;
            int n = eventosQueCumplenHoras.Count()-1;
            DateTime dateIn = new DateTime();

            if (n >= 0)
            {
                return Resta(dateIn, acum, n);
            }
            else
                return 0;
        }

        public override double Resta(DateTime dateIn, double acum, int n)
        {
            
            if (n >= 0)
            {
                State SigEstado = Di.siguienteEstado(n);
                DateTime dateF = Di.fechaEvento(n);

                return SigEstado.Resta(dateF, acum, n - 1);
            }
            else
                return acum;
            
        }

        public override float consumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {

        }

    }

    class Ahorro : State
    {
        // Constructor
        public Ahorro(State state)
        {
            DI = state.Di;
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


        public override double consumoEnHoras(float horas)
        {
            List<Evento> eventosQueCumplenHoras = Di.filtrarLista(horas);
            double acum = 0;
            int n = eventosQueCumplenHoras.Count()-1;
            DateTime dateIn = new DateTime();

            if (n >= 0)
            { 
                return Resta(dateIn, acum, n);
            }
            else
                return horas*1/3;
        }


        public override double Resta(DateTime dateIn, double acum, int n)
        {
            if (n >= 0)
            {
                State SigEstado = Di.siguienteEstado(n);
                DateTime dateF = Di.fechaEvento(n);
                TimeSpan time = (dateIn - dateF);
                acum += (time.TotalHours)/3;

                return SigEstado.Resta(dateF, acum, n - 1);

            }
            else
            {
                return acum;
            }
        }


        public override float consumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {

        }

    }


}