using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Mongo
    {
        private static MongoClient mongo;
        private Mongo() { }
        public static MongoClient getInstance()
        {
            if(mongo == null)
            {
                mongo = new MongoClient("mongodb://admin1:admin1@ds062097.mlab.com:62097/dbtp0");
                return mongo;
            }
            else {return mongo;}
        }
        public static List<Reporte> getReporte(string tipo, int id, DateTime fechaInicio, DateTime fechaFin)
        {
            var client = getInstance();
            var dbmongo = client.GetDatabase("dbtp0");
            var reportes = dbmongo.GetCollection<Reporte>("reportes");
            var builder = Builders<Reporte>.Filter;
            var filter = builder.Eq("tipoReporte", tipo) & builder.Eq("id", id) & builder.Eq("fechaInicio", fechaInicio) & builder.Eq("fechaFin", fechaFin);
            var reportesEncontrados = reportes.Find<Reporte>(filter);

            return reportesEncontrados.ToList<Reporte>();
        }
        public static void insertarReporte(Reporte r)
        {
            var client = getInstance();
            var dbmongo = client.GetDatabase("dbtp0");
            var reportes = dbmongo.GetCollection<Reporte>("reportes");
            reportes.InsertOne(r);
        }
    }
}