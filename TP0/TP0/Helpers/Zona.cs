using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Zona
    {
        [Key]
        public int ZonaID { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Radio { get; set; }
        double Energia { get; set; }

        //No se persiste en la db
        [NotMapped]
        public List<Transformador> transformadores;

        public Zona(int ID, double Lat, double Long, double Rad)
        {
            ZonaID = ID;
            Latitud = Lat;
            Longitud = Long;
            Radio = Rad;
        }

        public double ConsumoTotal(DateTime fInicial, DateTime fFinal)
        {
		    /*foreach (Transformador trafo in transformadores)
		    {
		        Energia += trafo.EnergiaQueEstaSuministrando(fInicial, fFinal);
		    }*/
            using (var db = new DBContext())
            {
                foreach (Transformador trafo in transformadores)
                {
                    Energia += trafo.EnergiaQueEstaSuministrando(fInicial, fFinal);
                }
            }
            return Energia;
	    }
    }
}