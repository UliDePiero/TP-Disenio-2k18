using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Usuario
    {
        [JsonProperty]
        public int id;

        public Administrador (Usuario u, int id)
        {
            this.id = id;
        }

        public int MesesQueLleva()
        {
            return Static.FechasAdmin.diferenciaDeMeses(DateTime.Parse(fechaDeAlta), DateTime.Now);
        }
    }
}