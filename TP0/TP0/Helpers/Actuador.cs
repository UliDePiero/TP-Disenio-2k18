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

        /*public Actuador(int dispositivoID)
        {
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
}