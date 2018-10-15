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

        public abstract void Encender(DispositivoInteligente d);

        public abstract void Apagar(DispositivoInteligente d);

        public abstract void AhorrarEnergia(DispositivoInteligente d);

        public abstract double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal);

        public void FinalizarEstado()
        {
            //Agrega la fecha final del ultimo estado del dispositivo
           using (var db = new DBContext()) //instancia del singleton
            {
               var est = db.Estados.Find(StateID);
               est.FechaFinal = DateTime.Now;
               db.SaveChanges();
            }
        }
    }

    public class Encendido : State
    {
        // Constructor
 
        public Encendido() { }

        public Encendido(DispositivoInteligente d)
        {
            FechaInicial = DateTime.Now;
            FechaFinal = new DateTime(3000, 1, 1);
            DispositivoID = d.DispositivoID;
            Desc = "Encendido";
        }

        public override void Encender(DispositivoInteligente d)
        {
            throw new Exception("El dispositivo ya esta encendido!");
        }

        public override void Apagar(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Apagado(d));
        }

        public override void AhorrarEnergia(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Ahorro(d));
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

        public Apagado() { }
        public Apagado(DispositivoInteligente d)
        {
            FechaFinal = new DateTime(3000, 1, 1);
            FechaInicial = DateTime.Now;
            DispositivoID = d.DispositivoID;
            Desc = "Apagado";
        }
        public Apagado(Dispositivo d)
        {
            FechaFinal = new DateTime(3000, 1, 1);
            FechaInicial = DateTime.Now;
            DispositivoID = d.DispositivoID;
            Desc = "Apagado";
        }

        public override void Encender(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Encendido(d));
        }

        public override void Apagar(DispositivoInteligente d)
        {
            throw new Exception("El dispositivo ya esta apagado!");
        }

        public override void AhorrarEnergia(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Ahorro(d));
        }

        public override double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
           return 0;
        }

    }

    public class Ahorro : State
    {
        // Constructor
        public Ahorro() { }
        public Ahorro(DispositivoInteligente d)
        {
            FechaFinal = new DateTime(3000, 1, 1);
            FechaInicial = DateTime.Now;
            DispositivoID = d.DispositivoID;
            Desc = "Ahorro";
        }

        public override void Encender(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Encendido(d));
        }

        public override void Apagar(DispositivoInteligente d)
        {
            FechaFinal = DateTime.Now;
            FinalizarEstado();
            d.AgregarEstado(new Apagado(d));
        }

        public override void AhorrarEnergia(DispositivoInteligente d)
        {
            throw new Exception("El dispositivo ya esta ahorrando!");
        }

        public override double ConsumoEnIntervalor(DateTime fInicial, DateTime fFinal)
        {
            double diff = (fFinal - fInicial).TotalHours*1/2;
            return diff;
        }
    }
}