using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public abstract class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string FechaDeAlta { get; set; }
        public string Username { get; set; }
        public string Contrasenia { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Telefono { get; set; }
        public bool EsAdmin { get; set; }
        public int PuntosAcum { get; set; }
        public bool AccionAutomatica { get; set; }

        public abstract int MesesQueLleva();
        public abstract bool AlgunDispositivoEncendido();
        public abstract int DispositivosEncendidos();
        public abstract int DispositivosApagados();
        public abstract int DispositivosEnAhorro();
        public abstract int DispositivosTotales();
        public abstract double EstimarFacturacion(DateTime fInicial, DateTime fFinal);
        public abstract double KwTotales(DateTime fInicial, DateTime fFinal);
        public abstract void AgregarDispInteligente(DispositivoInteligente DI);
        public abstract void AgregarDispEstandar(DispositivoEstandar DE);
        public abstract void AdaptarDispositivo(DispositivoEstandar D, string marca);
        public abstract RecomendacionXDisp[] SolicitarRecomendacion();
        public abstract void ActualizarCategoria();
       //public abstract List<double> UbicacionDomicilio();
       //public abstract double CalcDistancia(double[] punto1, double[] punto2);
        public void CambiarContraseña(string contra)
        {
            using (var db = new DBContext())
            {
                foreach (Usuario u in db.Usuarios)
                {
                    if (u.Username == Username)
                    {
                        u.Contrasenia = contra;
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
        public abstract void AgregarALaBase();
        public struct RecomendacionXDisp
        {
            public string NombreDispositivo;
            public double KWxHoraConsumidos;
            public string KWxHoraPuedeConsumir;
        }
    }
}