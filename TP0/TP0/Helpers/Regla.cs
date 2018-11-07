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
        public String Tipo { get; set; }  //(Humedad, luz, movimiento, temperatura)
        public String Descripcion { get; set; } //Que accion le indica al actuador que realice (apagar,encender,ahorro)
        [NotMapped]
        public bool SeCumple;

        public Regla(int id)
        {
            using (var db = new DBContext())
            {
                var r = db.Reglas.FirstOrDefault(re => re.ReglaID == id);
                ReglaID = r.ReglaID;
                ValorMax = r.ValorMax;
                ValorMin = r.ValorMin;
                ActuadorID = r.ActuadorID;
                SensorID = r.SensorID;
                Tipo = r.Tipo;
                Descripcion = r.Descripcion;
            }
        }
        public Regla(float valorMax, float valorMin, int actuadorID, int sensorID, String tipo, String descripcion)
        {
            ValorMax = valorMax;
            ValorMin = valorMin;
            ActuadorID = actuadorID;
            SensorID = sensorID;
            Tipo = tipo;
            Descripcion = descripcion; 
        }
        public Regla() { }

        public void Notificar(Medicion m)
        {
            //seCumple = false;
            if (m.Medida >= ValorMin && m.Medida <= ValorMax)
            {
                SeCumple = true;
                //Actuador.VerificarRegla();
                using (var db = new DBContext())
                    Actuador = db.Actuadores.FirstOrDefault(a => a.ActuadorID == ActuadorID);
                Actuador.EjecutarRegla(this);
            }
            else
            {
                SeCumple = false;
            }
                
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