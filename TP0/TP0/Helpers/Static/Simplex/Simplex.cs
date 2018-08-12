using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static.Simplex
{
    public class Simplex
    {
        public List<Double> vars { get; set; }
        public List<restriction> restrictions{ get; set; }
        public List<Double> getVars(int cantidad)
        {
            List<Double> valores = new List<Double>(50);
            int i = 0;
            while (i < cantidad)
            {
                valores.Add(1);
                i++;
            }
            return valores;
        }
    }
}