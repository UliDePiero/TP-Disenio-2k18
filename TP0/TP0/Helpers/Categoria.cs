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

        public Categoria()
        {
        }

        public Categoria(double consMin, double consMax, double cFijo, double cVariable)
        {
            consumoMax = consMax;
            consumoMin = consMin;
            cargoFijo = cFijo;
            cargoVariable = cVariable;
        }

        public double CalcularTarifa(double consumo)
        {
            return Math.Round(cargoFijo + consumo * cargoVariable, 2);
        }

        public bool PerteneceA(double consumo){
            return consumoMin <= consumo && consumo < consumoMax;
        }
    }
}
