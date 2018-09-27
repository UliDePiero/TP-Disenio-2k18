using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class State
    {
        [Key]
        public int StateID { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public DispositivoInteligente Dispositivo { get; set; }

        public string Desc { get; set; }


        public abstract void Encender();

        public abstract void Apagar();

        public abstract void AhorrarEnergia();

        public abstract double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal);

    }

    public class Encendido : State
    {
        // Constructor
        public Encendido(DispositivoInteligente dispint)
        {
            FechaInicial = DateTime.Now;
            Dispositivo = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }
        public override void Encender() { }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Apagado(Dispositivo));
        }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Ahorro(Dispositivo));
        }

        public override double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal)
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
            Dispositivo = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Encendido(Dispositivo));
        }

        public override void Apagar() { }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Ahorro(Dispositivo));
        }

        public override double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal)
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
            Dispositivo = dispint;
            FechaFinal = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Encendido(Dispositivo));
        }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            Dispositivo.AgregarEstado(new Apagado(Dispositivo));
        }

        public override void AhorrarEnergia() { }

        public override double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/3;
            return diff;
        }


    }
}