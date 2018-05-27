using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Usuario
    {
        public int id;
        public int MesesQueLleva()
        {
            return fAdmin.HaceCuantosMeses(DateTime.Parse(fechaDeAlta));
        }
    }
}