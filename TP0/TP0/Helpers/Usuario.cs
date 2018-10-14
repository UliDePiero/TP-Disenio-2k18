using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;

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

        public abstract int MesesQueLleva();
        public abstract bool AlgunDispositivoEncendido();
        public abstract int DispositivosEncendidos();
        public abstract int DispositivosApagados();
        public abstract int DispositivosTotales();
        public abstract double EstimarFacturacion(DateTime fInicial, DateTime fFinal);
        public abstract double KwTotales(DateTime fInicial, DateTime fFinal);
        public abstract void AgregarDispInteligente(DispositivoInteligente DI);
        public abstract void AdaptarDispositivo(DispositivoEstandar D, string marca);
        public abstract string SolicitarRecomendacion();
        public abstract void ActualizarCategoria();
        public abstract double[] UbicacionDomicilio();
        public abstract double CalcDistancia(double[] punto1, double[] punto2);

    }
}