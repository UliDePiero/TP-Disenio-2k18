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
        [NotMapped]
        public bool seCumple;

        [Key]
<<<<<<< HEAD
        public int ReglaID { get; set; }
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public Dispositivo Dispositivo { get; set; }
        public List<Condicion> Condiciones { get; set; }
=======
        public int CondicionID { get; set; }
        public float ValorMax { get; set; }
        public float ValorMin { get; set; }
        public int ActuadorID { get; set; }
        [ForeignKey("ActuadorID")]
        public Actuador Actuador { get; set; }

>>>>>>> parent of 3d157fb... Revert "Cambios Regla Actuador Sensor"

        public void Notificar(float valor)
        {
            seCumple = false;
            if (valor >= ValorMin && valor <= ValorMax)
            {
                seCumple = true;
            }
            Actuador.VerificarRegla();
        }
        public bool ChequearComportamiento()
        {
            return seCumple;
        }
    }
}