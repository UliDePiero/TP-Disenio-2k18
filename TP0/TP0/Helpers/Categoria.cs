using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Categoria
    {
        public int consumoMin;
        public int consumoMax;
        public float cargoFijo;      //  $/mes
        public float cargoVariable;  //  $/kWh
    }
}