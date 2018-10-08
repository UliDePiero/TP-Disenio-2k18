using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    // UNA REGLA ES UN CONJUNTO DE CONDICIONES SIMPLES
    public class Actuador
    {
        [Key]
        public int ActuadorID { get; set; }
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public Dispositivo Dispositivo { get; set; }
        //public List<Regla> Reglas { get; set; }
        public Regla Regla { get; set; }

        public void VerificarRegla()
        {
            //if (Reglas.All(c => c.seCumple))
            //{
                EjecutarRegla();
            //}
        }
        public void EjecutarRegla()
        {
            //Metodo para ejecutar la regla
        }
        public void Notificar(float valor)
        {
            //Reglas.ForEach(o => o.Notificar(valor));
            Regla.Notificar(valor);
        }
    }
}