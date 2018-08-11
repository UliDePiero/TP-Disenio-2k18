using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static.Simplex
{
    public class vars
    {
        public List<Double> valores { get; set; }
        public vars(int cantidad)
        {
            int i = 0;
            while (i < cantidad)
            {
                valores.Add(1);
            }
        }
    }
}