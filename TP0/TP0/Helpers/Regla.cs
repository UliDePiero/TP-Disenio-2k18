using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Regla
    {
        /*[NotMapped]
        public bool seCumple;*/

        [Key]
        public int ReglaID { get; set; }
        public float ValorMax { get; set; }
        public float ValorMin { get; set; }
        public int ActuadorID { get; set; }
        [ForeignKey("ActuadorID")]
        public Actuador Actuador { get; set; }
        public int SensorID { get; set; }
        [ForeignKey("SensorID")]
        public Sensor Sensor { get; set; }
        public String Tipo { get; set; }
        public String Descripcion { get; set; } //Que accion le indica al actuador que realice (apagar,encender,ahorro)

        public Regla(float valorMax, float valorMin, int actuadorID, int sensorID, String tipo, String descripcion)
        {
            ValorMax = valorMax;
            ValorMin = valorMin;
            ActuadorID = actuadorID;
            SensorID = sensorID;
            Tipo = tipo;
            Descripcion = descripcion; 
        }

        public void Notificar(Medicion m)
        {
            //seCumple = false;
            if (m.Medida >= ValorMin && m.Medida <= ValorMax)
                //seCumple = true;
                //Actuador.VerificarRegla();
                Actuador.EjecutarRegla(this);
        }

        public void CargarActuador()
        {
            using (var db = new DBContext())
            {
                Actuador = db.Actuadores.FirstOrDefault(a => a.ActuadorID == ActuadorID);
            }
        }
        /*public bool ChequearComportamiento()
        {
            return seCumple;
        }*/
    }
}