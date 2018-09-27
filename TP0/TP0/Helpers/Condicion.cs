using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Condicion
    {
#pragma warning disable CS0649 // Field 'Condicion.actuador' is never assigned to, and will always have its default value null
        [NotMapped]
        Actuador actuador;
#pragma warning restore CS0649 // Field 'Condicion.actuador' is never assigned to, and will always have its default value null
        [NotMapped]
        public bool seCumple;

        [Key]
        public int CondicionID { get; set; }
        public float ValorMax { get; set; }
        public float ValorMin { get; set; }

        public void Notificar(float valor)
        {
            seCumple = false;
            if (valor >= ValorMin && valor <= ValorMax)
            {
                seCumple = true;
            }
            actuador.VerificarRegla();
        }
        public bool ChequearComportamiento()
        {
            return seCumple;
        }
    }
}