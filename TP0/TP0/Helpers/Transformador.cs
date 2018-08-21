using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TP0.Helpers
{
    public class Transformador
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("zonaGeografica")]
        public Zona zonaGeografica { get; set; }

        [JsonProperty("latitude")]
        public double latitud { get; set; }

        [JsonProperty("longitude")]
        public double longitud { get; set; }

        [JsonProperty("energiaTotal")]
        public double energiaTotal { get; set; }

        [JsonProperty("clientes")]
        public List<Cliente> clientes { get; set; }
        //public List<Cliente> clientes;

        public Transformador(int id, double latitud, double longitud, double energiaTotal)
        {
            this.id = id;
            this.latitud = latitud;
            this.longitud = longitud;
            this.energiaTotal = energiaTotal;
        }

            public double energiaQueEstaSuministrando(DateTime fInicial, DateTime fFinal)
        {
            foreach (Cliente cli in clientes)
		    {
		    energiaTotal += cli.KwTotales(fInicial, fFinal);
		    }
	        return energiaTotal;
	    }
    }
}