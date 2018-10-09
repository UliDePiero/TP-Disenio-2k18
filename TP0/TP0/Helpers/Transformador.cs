using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Transformador
    {
        [JsonProperty("id")]
        [Key]
        public int TransformadorID { get; set; }

        public int ZonaID { get; set; }
        [JsonProperty("zonaGeografica")]
        [ForeignKey("ZonaID")]
        public Zona ZonaGeografica { get; set; }

        [JsonProperty("latitude")]
        public double Latitud { get; set; }
        [JsonProperty("longitude")]
        public double Longitud { get; set; }
        [JsonProperty("energiaTotal")]
        public double EnergiaTotal { get; set; }

        //No se persiste en la db
        [NotMapped]
        [JsonProperty("clientes")]
        public List<Cliente> Clientes { get; set; }
        //public List<Cliente> clientes;

        public Transformador(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud; 
        }
        public Transformador(int id, int Zonaid, double latitud, double longitud, int Energia)
        {
            TransformadorID = id;
            ZonaID = Zonaid;
            EnergiaTotal = Energia;
            Latitud = latitud;
            Longitud = longitud;
        }

        public double EnergiaQueEstaSuministrando(DateTime fInicial, DateTime fFinal)
        {
            double[] CoordUbicacion = new double[] { Latitud, Longitud };
            /*foreach (Cliente cli in Clientes)
		    {
		        EnergiaTotal += cli.KwTotales(fInicial, fFinal);
		    }*/
            using (var db = new DBContext())
            {
                foreach (Cliente cli in db.Usuarios)
                {
                    if(cli.TransformadorID==TransformadorID)
                    {
                        EnergiaTotal += cli.KwTotales(fInicial, fFinal);
                    }
                }
            }
            return EnergiaTotal;
	    }
    }
}