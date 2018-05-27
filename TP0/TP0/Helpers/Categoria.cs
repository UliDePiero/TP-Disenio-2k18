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
        public float consumoMin;
        [JsonProperty]
        public float consumoMax;
        [JsonProperty]
        public float cargoFijo;      //  $/mes
        [JsonProperty]
        public float cargoVariable;  //  $/kWh
        public float CalcularTarifa(float consumo)
        {
            return (cargoFijo + consumo * cargoVariable);
        }
        public bool PerteneceA(float consumo){
            return consumoMin <= consumo && consumo <= consumoMax;
        }
    }
}
