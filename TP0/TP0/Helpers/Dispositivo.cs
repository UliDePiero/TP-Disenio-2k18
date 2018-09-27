using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Dispositivo
    {
        [Key]
        public int DispositivoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double KWxHora { get; set; }
        public bool EsInteligente { get; set; }
    }
}