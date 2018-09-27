using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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

        public Transformador(int id, double latitud, double longitud, double energiaTotal)
        {
            TransformadorID = id;
            Latitud = latitud;
            Longitud = longitud;
            EnergiaTotal = energiaTotal;
        }

        public double EnergiaQueEstaSuministrando(DateTime fInicial, DateTime fFinal)
        {
            foreach (Cliente cli in Clientes)
		    {
		        EnergiaTotal += cli.KwTotales(fInicial, fFinal);
		    }
	        return EnergiaTotal;
	    }
    }
}