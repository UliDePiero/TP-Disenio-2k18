using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;


namespace TP0.Helpers
{
    public class JsonTrafos
    {
        string JsonPath = "file.json";

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader(JsonPath))
            {
                string json = r.ReadToEnd();
                List<Transformador> items = JsonConvert.DeserializeObject<List<Transformador>>(json);
                //JsonConvert.DeserializeObject<IEnumerable<T>>()
            }
        }

    }
}