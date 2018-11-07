using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class Actuador
    {
        
        [Key]
        public int ActuadorID { get; set; }
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public Dispositivo Dispositivo { get; set; }
        [NotMapped]
        public List<Regla> Reglas { get; set; }

        public void AgregarRegla(Regla r)
        {
            Reglas.Add(r);
        }

        public abstract void EjecutarRegla(Regla regla);
        /*{
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }*/
    }
    public class ActuadorLuz : Actuador
    {
        public ActuadorLuz() { }
        public ActuadorLuz(int dispositivoID)
        {          
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();

        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
            DispositivoInteligente d = new DispositivoInteligente(DispositivoID);
            switch (regla.Descripcion)
            {
                case "encender":
                    d.Encender();
                    break;
                case "apagar":
                    d.Apagar();
                    break;
                case "ahorro":
                    d.AhorrarEnergia();
                    break;                
            }
        }
    }
    public class ActuadorHumedad : Actuador
    {
        public ActuadorHumedad() { }
        public ActuadorHumedad(int dispositivoID)
        {
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
            DispositivoInteligente d = new DispositivoInteligente(DispositivoID);
            switch (regla.Descripcion)
            {
                case "encender":
                    d.Encender();
                    break;
                case "apagar":
                    d.Apagar();
                    break;
                case "ahorro":
                    d.AhorrarEnergia();
                    break;
            }
        }
    }
    public class ActuadorMovimiento : Actuador
    {
        public ActuadorMovimiento() { }
        public ActuadorMovimiento(int dispositivoID)
        {
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
            DispositivoInteligente d = new DispositivoInteligente(DispositivoID);
            switch (regla.Descripcion)
            {
                case "encender":
                    d.Encender();
                    break;
                case "apagar":
                    d.Apagar();
                    break;
                case "ahorro":
                    d.AhorrarEnergia();
                    break;
            }
        }
    }
     public class ActuadorTemperatura : Actuador
    {
        public ActuadorTemperatura() { }
        public ActuadorTemperatura(int dispositivoID)
        {
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
            DispositivoInteligente d = new DispositivoInteligente(DispositivoID);
            switch (regla.Descripcion)
            {
                case "encender":
                    d.Encender();
                    break;
                case "apagar":
                    d.Apagar();
                    break;
                case "ahorro":
                    d.AhorrarEnergia();
                    break;
            }
        }
    }
}