using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Usuario
    {

        public Administrador (Usuario u)
        {
            this.EsAdmin = true;
        }

        public int MesesQueLleva()
        {
            return Static.FechasAdmin.diferenciaDeMeses(DateTime.Parse(FechaDeAlta), DateTime.Now);
        }
    }
}