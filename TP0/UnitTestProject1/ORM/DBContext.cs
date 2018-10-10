using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using TP0.Helpers;

namespace UnitTestProject1.ORM
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

        public DBContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBDisenio;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }
    }
}
