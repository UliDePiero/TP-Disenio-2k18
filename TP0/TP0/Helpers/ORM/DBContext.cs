using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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
        public DbSet<Condicion> Condiciones { get; set; }

        public DBContext() : base(@"Data Source=(localdb)\v11.0;Initial Catalog=TPIdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }
    }
}