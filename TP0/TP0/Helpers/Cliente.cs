using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers;
using TP0.Helpers.ORM;
using GoogleMaps.LocationServices;

namespace TP0.Helpers
{
    public class Cliente : Usuario
    {
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Telefono { get; set; }
        public ICollection<Dispositivo> Dispositivos { get; set; }

        public int TransformadorID { get; set; }
        [ForeignKey("TransformadorID")]
        public Transformador Transformador { get; set; }

        //Datos que no se persisten en la base de datos
        [NotMapped]
        public Categoria categoria;
        [NotMapped]
        public int puntos;
        [NotMapped]
        public Recomendacion recomendacion = Recomendacion.Instancia();
        [NotMapped]
        public bool accionAutomatica; //porque no persiste?

        public Cliente(string nombre, string apellido, string domicilio, string usuario, string contrasenia, string doc, string tipo, string tel) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Domicilio = domicilio;
            Username = usuario;
            Contrasenia = contrasenia;
            EsAdmin = false;
            Documento = doc;
            TipoDocumento = tipo;
            Telefono = tel;
            Dispositivos = new List<Dispositivo>();
            recomendacion.NuevoCliente(this);
            accionAutomatica = false;
            FechaDeAlta = DateTime.Now.ToShortDateString();
        }
        public Cliente()
        {
        }
         
        public bool AlgunDispositivoEncendido()
        {
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    return true;
            }
            return false;
        }
        public int DispositivosEncendidos()
        {
            int encendidos = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    encendidos++;
            }
            return encendidos;
        }
        public int DispositivosApagados()
        {
            int apagados = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (!disp.EstaEncendido())
                    apagados++;
            }
            return apagados;
        }
        public int DispositivosTotales()
        {
            return Dispositivos.Count();
        }
        public double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            return categoria.CalcularTarifa(KwTotales(fInicial, fFinal));
        }
        public double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            double Consumo = 0;

            using (var db = new DBContext())
            {
                foreach (DispositivoEstandar d in db.Dispositivos)
                    Consumo += d.ConsumoEnPeriodo(fInicial, fFinal);
                foreach (DispositivoInteligente d in db.Dispositivos)
                    Consumo += d.ConsumoEnPeriodo(fInicial, fFinal);
            }
            return Consumo;
        }
        public void AgregarDispInteligente(DispositivoInteligente DI)
        {
            Dispositivos.Add(DI);
            puntos += 15;
            using (var db = new DBContext())
            {
                db.Dispositivos.Add(DI);
                db.Estados.Add(new Apagado(DI));
                db.SaveChanges();
            }
        }
        public void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            DispositivoInteligente DI;
            DI=D.ConvertirEnInteligente(marca);
            Dispositivos.Add(DI);
            puntos += 10;
            using (var db = new DBContext())
            {
                //Transforma el dispositivo en inteligente en la db
                var diDB = db.Dispositivos.First(d => d.UsuarioID == UsuarioID && d.Codigo == D.Codigo);
                diDB.EsInteligente = true;
                
                /*foreach(Dispositivo d in db.Dispositivos)
                {
                    if(d.UsuarioID == UsuarioID && d.Codigo == D.Codigo)
                    {
                        d.EsInteligente = true;
                    }
                }*/
                db.SaveChanges();
            }
        }

        public string SolicitarRecomendacion()
        {
            return recomendacion.GenerarRecomendacion(this);
        }

        public void ActualizarCategoria()
        {
            
        }

        public double[] UbicacionDomicilio()
        {
            var address = new AddressData { Address = Domicilio, City = "Buenos Aires", State = "Buenos Aires", Country = "Argentina" };
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            double[] CoordUbicacion = new double[] { latitude, longitude };
            return CoordUbicacion;
        }

        
        public static double CalcDistancia(double[] point1, double[] point2)
        {
            double radioTierra = 6371;
            double distance = 0;
            double Lat = Math.Abs(point2[0] - point1[0]) * (Math.PI / 180);
            double Lon = Math.Abs(point2[1] - point1[1]) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1[0] * (Math.PI / 180)) * Math.Cos(point2[0] * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = radioTierra * c;
            return distance;
        }
    }
}
