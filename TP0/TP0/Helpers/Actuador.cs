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
        public int ActuadorID { get; set; } //Hay que generar el ID del actuador?
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public Dispositivo Dispositivo { get; set; }
        [NotMapped]
        public List<Regla> Reglas { get; set; }

        /*public Actuador(int dispositivoID, int actuadorIDFAB)
        {
            ActuadorID = actuadorIDFAB;
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }*/

        public void AgregarRegla(Regla r)
        {
            Reglas.Add(r);
        }

        /*public void VerificarRegla()
        {
            //if (Reglas.All(c => c.seCumple))
            //{
                EjecutarRegla();
            //}
        }*/
        public abstract void EjecutarRegla(Regla regla);
        /*{
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }*/
        /*public void Notificar(float valor)
        {
            //Reglas.ForEach(o => o.Notificar(valor));
            //Regla.Notificar(valor);
        }*/
    }
    public class ActuadorLuz : Actuador
    {
        public ActuadorLuz(int dispositivoID/*, int actuadorIDFAB*/)
        {
            //ActuadorID = actuadorIDFAB;
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }
    }
    public class ActuadorHumedad : Actuador
    {
        public ActuadorHumedad(int dispositivoID/*, int actuadorIDFAB*/)
        {
            //ActuadorID = actuadorIDFAB;
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }
    }
    public class ActuadorMovimiento : Actuador
    {
        public ActuadorMovimiento(int dispositivoID/*, int actuadorIDFAB*/)
        {
            //ActuadorID = actuadorIDFAB;
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }
    }
     public class ActuadorTemperatura : Actuador
    {
        public ActuadorTemperatura(int dispositivoID/*, int actuadorIDFAB*/)
        {
            //ActuadorID = actuadorIDFAB;
            DispositivoID = dispositivoID;
            Reglas = new List<Regla>();
        }

        public override void EjecutarRegla(Regla regla)
        {
            //Metodo para ejecutar la regla
            //Metodo Strategy donde hay varias acciones que podria enviar al dispositivo de acuerdo a la regla
        }
    }
}