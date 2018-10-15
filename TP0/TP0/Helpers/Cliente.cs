using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers;
using TP0.Helpers.ORM;
using GoogleMaps.LocationServices;
using Geocoding.Google;
using Geocoding;
using System.Threading.Tasks;

namespace TP0.Helpers
{
    public class Cliente : Usuario
    {

        public ICollection<Dispositivo> Dispositivos { get; set; }

        public int TransformadorID { get; set; }
        [ForeignKey("TransformadorID")]

        public Transformador Transformador { get; set; }

        //Datos que no se persisten en la base de datos
        [NotMapped]
        public Categoria categoria;
        [NotMapped]
        public Recomendacion recomendacion = Recomendacion.Instancia();

        public Cliente()
        {
        }

        //constructor para crear nuevos
        public Cliente(string nombre, string apellido, string domicilio, string usuario, string contrasenia, string doc, string tipo, string tel) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Domicilio = domicilio;
            Username = usuario;
            Contrasenia = contrasenia;
            EsAdmin = false;
            Documento = doc;
            PuntosAcum = 0;
            AccionAutomatica = false;
            TipoDocumento = tipo;
            Telefono = tel;
            Dispositivos = new List<Dispositivo>();
            AccionAutomatica = false;
            FechaDeAlta = DateTime.Now.ToShortDateString();
        }

        public Cliente(string username) //para buscar en la DB + instanciar
        {
            using (var contexto = new DBContext())
            {
                var c = contexto.Usuarios.First(x => x.Username == username);
                Nombre = c.Nombre;
                Apellido = c.Apellido;
                Domicilio = c.Domicilio;
                Username = c.Username;
                Contrasenia = c.Contrasenia;
                EsAdmin = false;
                Documento = c.Documento;
                TipoDocumento = c.TipoDocumento;
                Telefono = c.Telefono;
                PuntosAcum = c.PuntosAcum;
                Dispositivos = new List<Dispositivo>();
                AccionAutomatica = c.AccionAutomatica;
                FechaDeAlta = c.FechaDeAlta;
                UsuarioID = c.UsuarioID; 
            }
        }

        public override bool AlgunDispositivoEncendido()
        {
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    return true;
            }
            return false;
        }
        public override int DispositivosEncendidos()
        {
            int encendidos = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    encendidos++;
            }
            return encendidos;
        }
        public override int DispositivosApagados()
        {
            int apagados = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (!disp.EstaEncendido())
                    apagados++;
            }
            return apagados;
        }
        public override int DispositivosTotales()
        {
            return Dispositivos.Count();
        }
        public override double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            return categoria.CalcularTarifa(KwTotales(fInicial, fFinal));
        }
        public override double KwTotales(DateTime fInicial, DateTime fFinal)
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
        public override void AgregarDispInteligente(DispositivoInteligente DI)
        {
            PuntosAcum += 15;
            using (var acuser = new DBContext())
            {
                var cldb = acuser.Usuarios.Find(UsuarioID);
                cldb.PuntosAcum += 15;
                acuser.SaveChanges();
            }//dejarlo cada uno en un bloque separado, si estan en el mismo bloque
             //por alguna razon mistica ROMPE el find del user.. <misteeeeeriooo>
             
            using (var db = new DBContext())
            {
                db.Dispositivos.Add(DI);
                db.SaveChanges();
                DI.AgregarEstado(new Apagado(DI));

            }
        }

        public void AccionAutomaticaON()
        {
            using (var db = new DBContext())
            {
                var acc = db.Usuarios.Find(UsuarioID);
                acc.AccionAutomatica = true;
                db.SaveChanges();
            }
        }
        public void AccionAutomaticaOFF()
        {
            using (var db = new DBContext())
            {
                var acc = db.Usuarios.Find(UsuarioID);
                acc.AccionAutomatica = false;
                db.SaveChanges();
            }
        }

        public override void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            var DI=D.ConvertirEnInteligente(marca);
            PuntosAcum += 10;

            using (var db = new DBContext())
            {
                var cldb = db.Usuarios.Find(UsuarioID);
                cldb.PuntosAcum += 10;
                var borrarDEst = db.Dispositivos.Find(D.DispositivoID);
                db.Dispositivos.Remove(borrarDEst);
                db.SaveChanges();

                db.Dispositivos.Add(DI);
                db.SaveChanges();
                DI.AgregarEstado(new Apagado(DI));
            }
        }

        public List<Dispositivo> GetDisps()
        {
            using (var db = new DBContext())
            {
                return db.Dispositivos.Where(x => x.UsuarioID == UsuarioID).ToList();
            }
        }

        public override string SolicitarRecomendacion()
        {
            return recomendacion.GenerarRecomendacion(this);
        }

        public override void ActualizarCategoria()
        {
            
        }

        //public double[] UbicacionDomicilio()
        public async Task<List<double>> UbicacionDomicilio()
        {
            var LatLong = new List<double>();
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "this-is-my-optional-google-api-key" };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(Domicilio + "Buenos Aires, Argentina");
            LatLong.Add(addresses.First().Coordinates.Latitude);
            LatLong.Add(addresses.First().Coordinates.Longitude);
            return LatLong;

            /*
            var ubicacion = new AddressData { Address = Domicilio, City = "Buenos Aires", State = "Buenos Aires", Country = "Argentina" };
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(ubicacion);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            double[] punto = new double[] { latitude, longitude };
            return punto;*/
        }

        public override double CalcDistancia(double[] punto1, double[] punto2)
        {
            double radioTierra = 6371;
            double distance = 0;
            double Lat = Math.Abs(punto2[0] - punto1[0]) * (Math.PI / 180);
            double Lon = Math.Abs(punto2[1] - punto1[1]) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(punto1[0] * (Math.PI / 180)) * Math.Cos(punto2[0] * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = radioTierra * c;
            return distance;
        }

        public override int MesesQueLleva()
        {
            throw new NotImplementedException();
        }
    }
}
