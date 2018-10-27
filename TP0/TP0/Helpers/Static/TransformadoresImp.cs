using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers.Static
{
    public static class TransformadoresImp
    {
        public static List<Transformador> transformadores;
        public static List<Transformador> GetTransformadores()
        {
            if (transformadores.Count > 0)
            {
                return transformadores;
            }
            else return null;
        }
        public static Transformador FiltrarTransformador(int idFiltro)
        {
            if (transformadores.Count > 0)
            {
                return transformadores.Find(x => x.TransformadorID == idFiltro);
            }
            else return null;
        }
        public static string GenerarJsonTransformadores (List<Transformador> transformadores)
        {
            string jsonData = JsonConvert.SerializeObject(transformadores);
            return jsonData;
        }
        public static void CargarNuevosTransformadores(List<Transformador> transformadores)
        {
            using (var db = new DBContext())
            {
                foreach(Transformador t in transformadores)
                {
                    if(!db.Transformadores.Any(tra => tra.TransformadorID == t.TransformadorID || (tra.Latitud == t.Latitud && tra.Longitud == t.Longitud)))
                    {
                        //Si no hay ningun transformador con el mismo id o las mismas coordenadas, se agrega.
                        db.Transformadores.Add(t);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
