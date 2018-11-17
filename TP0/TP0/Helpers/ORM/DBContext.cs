using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using TP0.Helpers.Static;


//es un singleton

namespace TP0.Helpers.ORM
{

    public class DBContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transformador> Transformadores { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<State> Estados { get; set; }
        public DbSet<Actuador> Actuadores { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Regla> Reglas { get; set; }
        public DbSet<Medicion> Mediciones { get; set; }
        public DbSet<DispositivoEstatico> DispEstaticos { get; set; }

        public DBContext() : base(@"Data Source=tcp:tpdds2018avengersdbserver.database.windows.net,1433;Initial Catalog=DBdisenio;User ID=sqladmin;Password=lalala231,.")
        {
        }
    }
}