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
        public FechasAdmin fAdmin;

        [JsonProperty]
        public int id;

    
        public int MesesQueLleva()
        {
            return fAdmin.diferenciaDeMeses(DateTime.Parse(fechaDeAlta), DateTime.Now);
        }
    }
}