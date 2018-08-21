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

        [JsonProperty("clientes")]
        public List<Cliente> clientes { get; set; }
        //public List<Cliente> clientes;

        [JsonProperty("zonaGeografica")]
        public Zona zonaGeografica { get; set; }

        [JsonProperty("latitude")]
        public double latitud { get; set; }

        [JsonProperty("longitude")]
        public double longitud { get; set; }

        [JsonProperty("energiaTotal")]
        public double energiaTotal { get; set; }

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