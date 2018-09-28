using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

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

        public void FinalizarEstado()
        {
            //Agrega la fecha final del ultimo estado del dispositivo
            using (var db = new DBContext())
            {
                foreach (State s in db.Estados)  
                {
                    if (s.DispositivoID == DispositivoID && s.FechaFinal == null)
                    {
                        s.FechaFinal = DateTime.Now;
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
    }

    public class Encendido : State
    {
        // Constructor
        public Encendido(DispositivoInteligente dispint)
        {
            FechaInicial = DateTime.Now;
            Dispositivo = dispint;
        }
        public override void Encender() { }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            Dispositivo.AgregarEstado(new Apagado(Dispositivo));
        }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
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
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            Dispositivo.AgregarEstado(new Encendido(Dispositivo));
        }

        public override void Apagar() { }

        public override void AhorrarEnergia()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
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
        }

        public override void Encender()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            Dispositivo.AgregarEstado(new Encendido(Dispositivo));
        }

        public override void Apagar()
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
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