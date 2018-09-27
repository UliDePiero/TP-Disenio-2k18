using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Medicion
    {
        [Key]
        public int MedicionID { get; set; }
        public DateTime DateTime { get; set; }
        public float Medida { get; set; }

        public int SensorID { get; set; }
        [ForeignKey("SensorID")]
        public int Sensor { get; set; }
    }
}