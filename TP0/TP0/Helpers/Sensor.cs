using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Sensor
    {
        [Key]
        public int SensorID { get; set; }

        public int CondicionID { get; set; }
        [ForeignKey("CondicionID")]
        public List<Condicion> Observers { get; set; }

        [NotMapped]
        public float Medicion;

        public void AgregarObservador(Condicion c)
        {
            Observers.Add(c);
        }
        public void QuitarObservador(Condicion c)
        {
            Observers.Remove(c);
        }
        public void Notificar()
        {
            Observers.ForEach(o => o.Notificar(Medicion));
        }
    }
}