using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    // UNA REGLA ES UN CONJUNTO DE CONDICIONES SIMPLES
    public class Regla
    {
        [Key]
        public int ActuadorID { get; set; }
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