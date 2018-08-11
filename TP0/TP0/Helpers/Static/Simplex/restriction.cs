using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static.Simplex
{
    public class restriction
    {
        public List<Double> values { get; set; }
        public string operador { get; set; }
        public restriction(List<Double> vs, string op)
        {
            values = vs;
            operador = op;
        }
    }
}