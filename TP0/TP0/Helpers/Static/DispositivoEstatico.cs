using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers.Static
{
    public class DispositivoEstatico
    {
        //Esta clase se usa para los disp que el usuario puede elegir

        [Key]
        public int DispositivoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double kWxHora { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public bool EsInteligente { get; set; }

        public DispositivoEstatico() { }
        public DispositivoEstatico(string cod, string nom, double kw, double min, double max, bool inteligente)
        {
            Codigo = cod;
            Nombre = nom;
            kWxHora = kw;
            Min = min;
            Max = max;
            EsInteligente = inteligente;
        }

        public void AgregarALaBase()
        {
            using (var db = new DBContext())
            {
                db.DispEstaticos.Add(this);
                db.SaveChanges();
            }
        }
    }
}