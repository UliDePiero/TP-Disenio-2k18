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
        public void AgregarALaBase()
        {
            //Agrega un usuario a la base
            //Solo se usa en registar
            using (var db = new DBContext())
            {
                db.Usuarios.Add(this);

                if (db.Zonas.Count() == 0)
                {
                    //Agrega el transformador default si no existe
                    db.Zonas.Add(new Zona(1, 1, 1, 1));
                    db.Transformadores.Add(new Transformador(1, 1, 1, 1, 1));
                }


                db.SaveChanges();
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
            foreach (Dispositivo disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    encendidos++;
            }
            return encendidos;
        }
        public override int DispositivosApagados()
        {
            int apagados = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                if (!disp.EstaEncendido())
                    apagados++;
            }
            return apagados;
        }
        public override int DispositivosEnAhorro()
        {
            int ahorro = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                if (disp.EnAhorro())
                    ahorro++;
            }
            return ahorro;
        }
        public override int DispositivosTotales()
        {
            return Dispositivos.Count();
        }

        public override void AgregarDispInteligente(DispositivoInteligente DI)
        {
            PuntosAcum += 15;
            using (var db = new DBContext())
            {
                foreach(Usuario u in db.Usuarios)
                {
                    if (u.Username == Username)
                    {
                        u.PuntosAcum += 15;
                        DI.UsuarioID = u.UsuarioID;
                        break;
                    }
                }
                db.Dispositivos.Add(DI);
                db.SaveChanges();
                DI.AgregarEstado(new Apagado(DI));
            }
        }
        public override void AgregarDispEstandar(DispositivoEstandar DE)
        {
            PuntosAcum += 15;
            using (var db = new DBContext())
            {
                foreach (Usuario u in db.Usuarios)
                {
                    if (u.Username == Username)
                    {
                        u.PuntosAcum += 15;
                        DE.UsuarioID = u.UsuarioID;
                        break;
                    }
                }
                db.Dispositivos.Add(DE);
                db.SaveChanges();
            }
        }

        public override void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            var DI = D.ConvertirEnInteligente(marca);
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

        public double TotalConsumo()
        {
            int DispInt = 0;
            double acumulado = 0;
            double acumuladoKw = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                disp.ConsumoAcumulado = 0;
                acumulado = 0;
                //Si es inteligente le asigna su estado actual
                if (disp is DispositivoInteligente)
                {
                    disp.Desc = new DispositivoInteligente(disp.DispositivoID).GetEstado().Desc;

                    List<State> listaDeEstados = disp.GetEstados();
                    foreach (State s in listaDeEstados)
                    {
                        double c;
                        switch (s.Desc)
                        {
                            case "Encendido":
                                if (s.FechaFinal != new DateTime(1, 1, 1))         //Si es el ultimo estado, se le pone como fecha final ahora
                                    c = (s.FechaFinal - s.FechaInicial).Minutes;
                                else
                                    c = (DateTime.Now - s.FechaInicial).Minutes;
                                acumulado += c;
                                acumuladoKw += c * disp.KWxHora / 60;
                                break;
                            case "Ahorro":
                                if (s.FechaFinal != new DateTime(1, 1, 1))
                                    c = (s.FechaFinal - s.FechaInicial).Minutes / 2; //En modo ahorro se consume la mitad de la energia
                                else
                                    c = (DateTime.Now - s.FechaInicial).Minutes / 2;
                                acumulado += c;
                                acumuladoKw += c * disp.KWxHora / 60;
                                break;
                        }
                    }
                    disp.ConsumoAcumulado = acumulado;
                    DispInt++;
                }
                //Si es estandar no se le asigna estado
            }
            return acumuladoKw / DispInt;
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
        
        public List<Dispositivo> GetDisps()
        {
            using (var db = new DBContext())
            {
                return db.Dispositivos.Where(x => x.UsuarioID == UsuarioID).ToList();
            }
        }
        public void CargarDisps()
        {
            Dispositivos.Clear();
            using (var db = new DBContext())
            {
                Dispositivos = db.Dispositivos.Where(x => x.UsuarioID == UsuarioID).ToList();
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
