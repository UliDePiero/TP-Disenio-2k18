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
        }
       
        public Sensor()
        {
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
            Reglas.ForEach(o => o.Notificar( ObtenerMedicion() ));
        }

        public void Medir(float valorMedicion, DateTime tFinal)
        {
            if(DateTime.Compare(DateTime.Now, tFinal) < 0)
            {
                ValorMedicion = valorMedicion;
                FechaUltimaMedicion = DateTime.Now;
                Notificar();
                Midiendo = true;
               //Medir(valorMedicion, tFinal);
            }                
            Midiendo = false;
        }

        public Medicion ObtenerMedicion()
        {
            UltimaMedicion.Medida = ValorMedicion;
            UltimaMedicion.Fecha = FechaUltimaMedicion;
            UltimaMedicion.SensorID = SensorID;
            Mediciones.Add(UltimaMedicion);
            using (var db = new DBContext())
            {
                db.Mediciones.Add(UltimaMedicion);
                db.SaveChanges();
            }
            return UltimaMedicion;
        }

        public void CargarReglas()
        {
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