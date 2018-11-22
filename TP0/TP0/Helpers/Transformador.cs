using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TP0.Helpers.ORM;
using GoogleMaps;

namespace TP0.Helpers
{
    public class Transformador
    {
        //[JsonProperty("id")]
        [Key]
        public int TransformadorID { get; set; }

        public int ZonaID { get; set; }
        //[JsonProperty("zonaGeografica")]
        [ForeignKey("ZonaID")]
        public Zona ZonaGeografica { get; set; }

        [JsonProperty("latitude")]
        public double Latitud { get; set; }
        [JsonProperty("longitude")]
        public double Longitud { get; set; }
        //[JsonProperty("energiaTotal")]
        public double EnergiaTotal { get; set; }

        //No se persiste en la db
        [NotMapped]
        [JsonProperty("clientes")]
        public List<Cliente> Clientes { get; set; }
        //public List<Cliente> clientes;

        public Transformador() { }
        public Transformador(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud; 
        }
        public Transformador(int id, int Zonaid, double latitud, double longitud, int Energia)
        {
            TransformadorID = id;
            ZonaID = Zonaid;
            EnergiaTotal = Energia;
            Latitud = latitud;
            Longitud = longitud;
        }
        public Transformador(int Zonaid, double latitud, double longitud, int Energia)
        {
            ZonaID = Zonaid;
            EnergiaTotal = Energia;
            Latitud = latitud;
            Longitud = longitud;
        }
        public double EnergiaQueEstaSuministrando(DateTime fInicial, DateTime fFinal)
        {
            //double[] CoordUbicacion = new double[] { Latitud, Longitud };
            EnergiaTotal = 0;
            using (var db = new DBContext())
            {
                var clientes = db.Usuarios.Where(c => c.EsAdmin == false).ToList();
                foreach (Cliente c in clientes)
                {
                    if (c.TransformadorID == TransformadorID)
                    {
                        EnergiaTotal += c.KwTotales(fInicial, fFinal);
                    }
                }
            }
            return EnergiaTotal;
	    }
        public void asignarZona()
        {
            
            using (var db = new DBContext())
            {
                foreach (var z in db.Zonas)
                {
                    if (CalcDistancia(z) <= z.Radio)
                    {
                        ZonaID = z.ZonaID;
                        return;
                    }
                }
                var zonaDefault = db.Zonas.First(z => z.Radio == 0.4);
                ZonaID = zonaDefault.ZonaID;
            }
        }

        public double CalcDistancia(Zona zona)
        {
            double radioTierra = 6371;
            double distance = 0;
            double Lat = Math.Abs(Latitud - zona.Latitud) * (Math.PI / 180);
            double Lon = Math.Abs(Longitud - zona.Longitud) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(zona.Latitud * (Math.PI / 180)) * Math.Cos(Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = Math.Round(radioTierra * c, 3);
            return distance;
        }

        public void ActualizarEnergiaQueEstaSuministrando()
        {
            EnergiaTotal = 0;
            using (var db = new DBContext())
            {
                var clientes = db.Usuarios.Where(c => c.EsAdmin == false).ToList();
                foreach (Cliente c in clientes)
                {
                    if (c.TransformadorID == TransformadorID)
                    {
                        c.CargarDisps();
                        EnergiaTotal += c.ConsumoActual();
                    }
                }
            }
        }
    }
}