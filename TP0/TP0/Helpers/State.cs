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
        public DispositivoInteligente DI;

        public abstract void Encender();

        public abstract void Apagar();

        public abstract void AhorrarEnergia();

        public abstract double consumoEnIntervalor(DateTime fInicial, DateTime fFinal);

    }

    public class Encendido : State
    {
        // Constructor
        public Encendido(DispositivoInteligente dispint)
        {
            FechaInicial = DateTime.Now;
            DI = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }
        public override void Encender() { }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Apagado(DI));
        }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Ahorro(DI));
        }

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours;
            return diff;
        }

    
    }

    public class Apagado : State
    {
        // Constructor
        public Apagado(DispositivoInteligente dispint)
        {
            FechaInicial = DateTime.Now;
            DI = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Encendido(DI));
        }

        public override void Apagar() { }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Ahorro(DI));
        }

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
           return 0;
        }

    }

    public class Ahorro : State
    {
        // Constructor
        public Ahorro(DispositivoInteligente dispint)
        {
            FechaInicial = DateTime.Now;
            DI = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Encendido(DI));
        }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            DI.agregarEstado(new Apagado(DI));
        }

        public override void AhorrarEnergia() { }

        public override double consumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/3;
            return diff;
        }


    }
}