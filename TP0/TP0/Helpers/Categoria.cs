using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Categoria
    {
        [JsonProperty]
        public double consumoMin;
        [JsonProperty]
        public double consumoMax;
        [JsonProperty]
        public double cargoFijo;      //  $/mes
        [JsonProperty]
        public double cargoVariable;  //  $/kWh

        public double CalcularTarifa(double consumo)
        {
            return (cargoFijo + consumo * cargoVariable);
        }

        public bool PerteneceA(double consumo){
            return consumoMin <= consumo && consumo <= consumoMax;
        }
    }
}
