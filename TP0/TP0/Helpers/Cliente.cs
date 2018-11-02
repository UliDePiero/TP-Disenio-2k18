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
using TP0.Helpers.Static;

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
            Dispositivos = new List<Dispositivo>(); //Creo la lista vacia
            AccionAutomatica = false;
            FechaDeAlta = DateTime.Now.ToShortDateString();
            ConectarseAlTrafoMasProx();
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
                EsAdmin = c.EsAdmin; //Esto debe levantarse de la base
                Documento = c.Documento;
                TipoDocumento = c.TipoDocumento;
                Telefono = c.Telefono;
                PuntosAcum = c.PuntosAcum;
                Dispositivos = new List<Dispositivo>();
                Dispositivos = GetDisps(); //levanto todos los dispositivos del cliente de una
                AccionAutomatica = c.AccionAutomatica;
                FechaDeAlta = c.FechaDeAlta;
                UsuarioID = c.UsuarioID; 
            }
        }
        public Cliente(int id) //para buscar en la DB + instanciar
        {
            using (var contexto = new DBContext())
            {
                var c = contexto.Usuarios.First(x => x.UsuarioID == id);
                Nombre = c.Nombre;
                Apellido = c.Apellido;
                Domicilio = c.Domicilio;
                Username = c.Username;
                Contrasenia = c.Contrasenia;
                EsAdmin = c.EsAdmin; //Esto debe levantarse de la base
                Documento = c.Documento;
                TipoDocumento = c.TipoDocumento;
                Telefono = c.Telefono;
                PuntosAcum = c.PuntosAcum;
                Dispositivos = new List<Dispositivo>();
                Dispositivos = GetDisps(); //levanto todos los dispositivos del cliente de una
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
                if (disp.EstaApagado())
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
        public int DispositivosEstandares()
        {
            int Estandares = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                if (!disp.EsInteligente)
                    Estandares++;
            }
            return Estandares;
        }
        public int DispositivosInteligentes()
        {
            int Inteligentes = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                if (disp.EsInteligente)
                    Inteligentes++;
            }
            return Inteligentes;
        }

        public void AgregarDispJson(List<DispositivoEstatico> disps)
        {
            //Metodo para agregar los disp que no estan ya en la base desde el json en FileUploadController
            List<DispositivoEstatico> agregar = new List<DispositivoEstatico>();
            foreach (DispositivoEstatico d in disps)
                if (!Dispositivos.Any(di => di.DispositivoID == d.DispositivoID))
                    agregar.Add(d);
            foreach (DispositivoEstatico d in agregar)
                if (d.EsInteligente)
                    AgregarDispInteligente(new DispositivoInteligente(d.Nombre, d.Codigo, d.kWxHora, d.Max, d.Min) { DispositivoID = d.DispositivoID });
                else
                    AgregarDispEstandar(new DispositivoEstandar(d.Nombre, d.Codigo, d.kWxHora, 0, d.Max, d.Min) { DispositivoID = d.DispositivoID });
        }
        public override void AgregarDispInteligente(DispositivoInteligente DI)
        {
            PuntosAcum += 15;
            Dispositivos.Add(DI);
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
            Dispositivos.Add(DE);
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
            int Disp = 0;
            double acumuladoKw = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                acumuladoKw += disp.Consumo();
                Disp++;
            }
            return acumuladoKw / Disp;
        }
        public double ConsumoActual()
        {
            //Devuelve el consumo en horas de todos los disp encendidos
            double consumo = 0;
            foreach (Dispositivo disp in Dispositivos)
            {
                consumo += disp.ConsumoActual();
            }
            return consumo;
        }
        public override double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            return categoria.CalcularTarifa(KwTotales(fInicial, fFinal));
        }
        public override double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            double Consumo = 0;
            Dispositivos = GetDisps();
            foreach (var disp in Dispositivos)
                    if (disp.EsInteligente)
                    {
                        var di = new DispositivoInteligente(disp.DispositivoID);
                        Consumo += di.ConsumoEnPeriodo(fInicial, fFinal);
                    }
                    else
                    {
                        var de = new DispositivoEstandar(disp.DispositivoID);
                        Consumo += de.ConsumoEnPeriodo(fInicial, fFinal);
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
            using (var db = new DBContext())
            {
                Dispositivos = db.Dispositivos.Where(x => x.UsuarioID == UsuarioID).ToList();
            }
            foreach (Dispositivo d in Dispositivos)
            {
                d.ActualizarUltimoEstado();
                try
                {
                    d.Desc = d.GetEstado().Desc;
                }
                catch (NotImplementedException e)
                {
                    //Reconoce que hay un error pero
                    //No hace nada porque los standar no tienen estados
                }
            }
        }


        public override RecomendacionXDisp[] SolicitarRecomendacion() //Convierte el string en una lista de doubles
        {

            int i = 1;
            var LDI = new List<Dispositivo>();
            var LDE = new List<Dispositivo>();

            var result = recomendacion.GenerarRecomendacion(this);
            double[] doubleV = recomendacion.ParsearString(result);
            foreach (var d in Dispositivos)
            {
                if (d.EsInteligente)
                    LDI.Add(d);
                else
                    LDE.Add(d);
                i++;
            }
            var DispsOrdernados = LDE.Concat(LDI); //Ordenar dispositivos: Primero LDI y despues los LDE
            var RecomendacionXDispositivos = new RecomendacionXDisp[i];

            int j=0;
            var tiempoTotal = new RecomendacionXDisp(); //Estructura
            tiempoTotal.NombreDispositivo = "Total acumulado";
            tiempoTotal.KWxHoraPuedeConsumir = doubleV[j];
            RecomendacionXDispositivos[0]=tiempoTotal;
            j++;

            double horasDelMes = DateTime.Now.Day*24;

            foreach (var disp in Dispositivos)
            {
                var recXdisp = new RecomendacionXDisp();
                recXdisp.NombreDispositivo = disp.Nombre;
                recXdisp.KWxHoraPuedeConsumir = doubleV[j];
                if (disp.EsInteligente)
                {
                    var di = new DispositivoInteligente(disp.DispositivoID);
                    recXdisp.KWxHoraConsumidos = di.ConsumoEnHoras(horasDelMes);
                }
                else
                {
                    var de = new DispositivoEstandar(disp.DispositivoID);
                    recXdisp.KWxHoraConsumidos = de.ConsumoEnHoras(horasDelMes);
                }
                RecomendacionXDispositivos[j] = recXdisp;
                j++;
            }

            return RecomendacionXDispositivos;
    }

    public override void ActualizarCategoria()
        {
            
        }

        public void ConectarseAlTrafoMasProx()
        {
            var point = UbicacionDomicilio();
            using (var context = new DBContext())
            {
                double dmin=999999; //poner un high value
                foreach (Transformador t in context.Transformadores)
                {
                    var d = CalcDistancia(point, t.Latitud, t.Longitud);
                    if(d<dmin)
                    {
                        TransformadorID = t.TransformadorID;
                        dmin = d;
                    }
                }
            }
        }

        public double[] UbicacionDomicilio()
        {
            var direccion = new AddressData { Address = Domicilio, City = "CABA", State = "Buenos Aires", Country = "Argentina" };
            var locationService = new GoogleLocationService("AIzaSyAsnYLI5zaV64sqtymtTCb7jSdBC0xK5Kk");
            var point = locationService.GetLatLongFromAddress(direccion);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            double[] punto = new double[] { latitude, longitude };
            return punto;
        }

        public double CalcDistancia(double[] casa, double latTrafo, double longTrafo)
        {
            double radioTierra = 6371;
            double distance = 0;
            double Lat = Math.Abs(latTrafo - casa[0]) * (Math.PI / 180);
            double Lon = Math.Abs(longTrafo - casa[1]) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(casa[0] * (Math.PI / 180)) * Math.Cos(latTrafo * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = Math.Round(radioTierra * c,3);
            return distance;
        }

        public override int MesesQueLleva()
        {
            throw new NotImplementedException();
        }

    }

}
