using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Regla
    {
        /*[NotMapped]
        public bool seCumple;*/

        public Regla(float valorMax, float valorMin, int actuadorID)
        {
            ValorMax = valorMax;
            ValorMin = valorMin;
            ActuadorID = actuadorID;
        }

        [Key]
        public int ReglaID { get; set; }
        public float ValorMax { get; set; }
        public float ValorMin { get; set; }
        public int ActuadorID { get; set; }
        [ForeignKey("ActuadorID")]
        public Actuador Actuador { get; set; }


        public void Notificar(Medicion m)
        {
            //seCumple = false;
            if (m.Medida >= ValorMin && m.Medida <= ValorMax)
                //seCumple = true;
                //Actuador.VerificarRegla();
                Actuador.EjecutarRegla(this);
        }
        /*public bool ChequearComportamiento()
        {
            return seCumple;
        }*/
    }
}