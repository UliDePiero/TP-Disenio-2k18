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
        public override void AgregarDispInteligente(Dispositivo DI)
        {
            //Dispositivos.Add(DI);
            puntos += 15;
            using (var db = DBContext.Instancia())
            {
                DI.UsuarioID = UsuarioID;
                db.Dispositivos.Add(DI);
                db.Estados.Add(new Apagado(DI));
                db.SaveChanges();
            }
        }
        public override void AdaptarDispositivo(Dispositivo D, string marca)
        {
            var DI=D.ConvertirEnInteligente(marca);
            Dispositivos.Add(DI);
            puntos += 10;

            using (var db = DBContext.Instancia())
            {
                //Transforma el dispositivo en inteligente en la db
                db.Dispositivos.Add(DI);
                var dDB = db.Dispositivos.First(x => x.DispositivoID == D.DispositivoID);
                db.Dispositivos.Remove(dDB);
                
                db.SaveChanges();

            }
        }

        public override string SolicitarRecomendacion()
        {
            return recomendacion.GenerarRecomendacion(this);
        }

        public override void ActualizarCategoria()
        {
            
        }

        public override double[] UbicacionDomicilio()
        {
            var address = new AddressData { Address = Domicilio, City = "Buenos Aires", State = "Buenos Aires", Country = "Argentina" };
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            double[] punto = new double[] { latitude, longitude };
            return punto;
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
