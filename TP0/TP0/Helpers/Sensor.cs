using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Sensor
    {
        [Key]
        public int SensorID { get; set; }
        public int UsuarioID { get; set; }
       // [ForeignKey("UsuarioID")]
      //  public Usuario Usuario { get; set; }
        public string Desc { get; set; }
        [NotMapped]
        public List<Regla> Reglas { get; set; }
        [NotMapped]
        public bool Midiendo { get; set; }
        [NotMapped]
        public float ValorMedicion { get; set; }
        [NotMapped]
        public DateTime FechaUltimaMedicion { get; private set; }
        [NotMapped]
        public List<Medicion> Mediciones;   //(Como seria guardar N mediciones??? Con un metodo???)
        [NotMapped]
        public Medicion UltimaMedicion;

        public Sensor(string descripcion, int usuarioID)
        {
            Desc = descripcion;
            UsuarioID = usuarioID;
            Reglas = new List<Regla>();
        }
       
        public Sensor()
        {
            Reglas = new List<Regla>();
        }
        
        public void AgregarRegla(Regla c)
        {
            c.SensorID = SensorID;
            Reglas.Add(c);
            using (var db = new DBContext())
            {
                db.Reglas.Add(c);               
                db.SaveChanges();
            }
        }
        public void QuitarRegla(Regla c)
        {
            Reglas.Remove(c);
            using (var db = new DBContext())
            {
                db.Reglas.Remove(c);               
                db.SaveChanges();
            }
        }
        public void Notificar()
        {
            //Observers.ForEach(o => o.Notificar(UltimaMedicion));
            Reglas.ForEach(o => o.Notificar( UltimaMedicion ));
        }

        public void Medir(float valorMedicion, DateTime tFinal)
        {
            if(DateTime.Compare(DateTime.Now, tFinal) <= 0)
            {
                ValorMedicion = valorMedicion;
                FechaUltimaMedicion = DateTime.Now;
                UltimaMedicion = new Medicion(FechaUltimaMedicion, valorMedicion, SensorID);
                using (var db = new DBContext())
                {
                    db.Mediciones.Add(UltimaMedicion);
                    db.SaveChanges();
                }
                Notificar();
                Midiendo = true;
               //Medir(valorMedicion, tFinal);
            }                
            Midiendo = false;
        }

        public List<Medicion> ObtenerMediciones()
        {
            Mediciones.Clear();
            CargarMediciones();
            return Mediciones;
        }
        public void CargarMediciones()
        {
            using (var db = new DBContext())
            {
                foreach (Medicion m in db.Mediciones)
                    if (m.SensorID == SensorID)
                        Mediciones.Add(m);
            }
        }

        public void CargarReglas()
        {
            Reglas = new List<Regla>();
            using (var db = new DBContext())
            {
                foreach (Regla r in db.Reglas)
                    if (r.SensorID == SensorID)
                        Reglas.Add(r);
            }
        }
    }
    /*
    public class SensorHumedad : Sensor
    {
        public SensorHumedad(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorHumedad()
        {
        }
    }
    public class SensorMovimiento : Sensor
    {
        public SensorMovimiento(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorMovimiento()
        {
        }
    }
    public class SensorTemperatura : Sensor
    {
        public SensorTemperatura(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorTemperatura()
        {
        }
    }
    public class SensorLuz : Sensor
    {
        public SensorLuz(string descripcion)
        {
            Desc = descripcion;
        }
        public SensorLuz()
        {
        }
    }
    */
}