using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public abstract class Sensor
    {
        [Key]
        public int SensorID { get; set; }
        public string Desc { get; set; }
        public bool Midiendo { get; set; }
        public float ValorMedicion { get; set; }
        public DateTime FechaUltimaMedicion { get; private set; }
        [NotMapped]
        public List<Regla> Observers { get; set; }
        [NotMapped]
        public List<Medicion> Mediciones;   //(Como seria guardar N mediciones??? Con un metodo???)
        public Medicion UltimaMedicion;

        public Sensor(string descripcion) //Hay que generar el ID del sensor?
        {
            Desc = descripcion;
            using (var db = new DBContext())
            {
                db.Sensores.Add(this);
                db.SaveChanges();
            }
        }
        public Sensor()
        {
        }
        
        public void AgregarObservador(Regla c)
        {
            Observers.Add(c);
            using (var db = new DBContext())
            {
                db.Reglas.Add(c);
                db.Actuadores.Add(c.Actuador);
                db.SaveChanges();
            }
        }
        public void QuitarObservador(Regla c)
        {
            Observers.Remove(c);
            using (var db = new DBContext())
            {
                db.Reglas.Remove(c);
                db.Actuadores.Remove(c.Actuador);
                db.SaveChanges();
            }
        }
        public void Notificar()
        {
            //Observers.ForEach(o => o.Notificar(UltimaMedicion));
            Observers.ForEach(o => o.Notificar( ObtenerMedicion() ));
        }

        public void Medir(float valorMedicion, DateTime tFinal)
        {
            if(DateTime.Compare(DateTime.Now, tFinal) < 0)
            {
                ValorMedicion = valorMedicion;
                FechaUltimaMedicion = DateTime.Now;
                Notificar();
                Midiendo = true;
                Medir(valorMedicion, tFinal);
            }                
            Midiendo = false;
        }

        public Medicion ObtenerMedicion() //Hay que generar el ID de la medicion?
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