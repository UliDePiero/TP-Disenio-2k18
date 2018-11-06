using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers.Static
{
    public class SensorEstatico
    {
        [Key]
        public int SensorID { get; set; }
        
        public int DispositivoID { get; set; }
        [ForeignKey("DispositivoID")]
        public DispositivoEstatico Dispositivo { get; set; }
        public String Descripcion { get; set; }

        public SensorEstatico() { }
        public SensorEstatico(string descripcion, int dispositivoID)
        {
            Descripcion = descripcion;
            DispositivoID = dispositivoID;
        }
        public SensorEstatico(string descripcion, DispositivoEstatico dispositivo)
        {
            Descripcion = descripcion;
            Dispositivo = dispositivo;
        }

        public void AgregarALaBase()
        {
            using (var db = new DBContext())
            {
                db.SensoresEstaticos.Add(this);
                db.SaveChanges();
            }
        }
    }
}