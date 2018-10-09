using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    // UN ACTUADOR ES UN CONJUNTO DE REGLAS SIMPLES
    public class Actuador
    {
        [Key]
        public int ReglaID { get; set; }
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public Dispositivo Dispositivo { get; set; }
        public List<Condicion> Condiciones { get; set; }

        public void VerificarRegla()
        {
            if (Condiciones.All(c => c.seCumple))
            {
                EjecutarRegla();
            }
        }
        public void EjecutarRegla()
        {
            //Metodo para ejecutar la regla
        }
        public void Notificar(float valor)
        {
            Condiciones.ForEach(o => o.Notificar(valor));
        }
    }
}