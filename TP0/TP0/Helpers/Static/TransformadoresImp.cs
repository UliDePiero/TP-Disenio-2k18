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
        public static List<Transformador> transformadoresEnDb()
        {
            using(var db = new DBContext())
            {
                List<Transformador> transformadores = db.Transformadores.ToList();
                return transformadores;
            }
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
                    if(!db.Transformadores.Any(tra => /*tra.TransformadorID == t.TransformadorID || */ (tra.Latitud == t.Latitud && tra.Longitud == t.Longitud)))
                    {
                        //Si no hay ningun transformador con el mismo id o las mismas coordenadas, se agrega.
                        //El id debe saberse? si es un trafo q se carga por primera vez como se sabe el id?
                        //Al igual que la zona id, se asignan al momento de subir el transoformador en base a su latitud y longitud
                        t.asignarZona(); //metodo para calcular a q ZONA pertenece
                        //t.ZonaID = 1;
                        db.Transformadores.Add(t);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
