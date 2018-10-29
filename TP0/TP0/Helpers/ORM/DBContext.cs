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

    class Program
    {
        static void Main()
        {
            using (var db = DBContext.Instancia())
            {
                Console.Write("Caso de prueba:");
                var flag = Convert.ToInt32(Console.ReadLine());

                switch (flag)
                {
                    case 1:
                        /*Caso de prueba 1: Crear 1 usuario nuevo. Persistirlo. 
                         *Recuperarlo, modificar la geolocalización y grabarlo. 
                         *Recuperarlo y evaluar que el cambio se haya realizado.*/

                        Console.Write("Creando usuario: Juanito");
                        var user = new Cliente("Juan", "Cuevas", "Medrano 951" , "Juanito", "clavoUnClavito123", "44838254", "DNI", "2342342");

                        db.Usuarios.Add(user);
                        db.SaveChanges();

                        var usr = db.Usuarios.First(u => u.Username == "Juanito");
                            //usr.geolocalizacion = ?? //donde deberia hacerse el cambio de geolocalizacion
                            db.SaveChanges();
                        

                        var usrDos = db.Usuarios.First(u => u.Username == "Juanito");

                        //Console.WriteLine("La nueva localizacion de {0} es: {1}", usr.Username, usr.geolocalizacion);

                        break;

                    case 2:
                        /*Recuperar un dispositivo. Mostrar por consola todos los intervalos que estuvo
                          encendido durante el último mes. Modificar su nombre (o cualquier otro atributo
                          editable) y grabarlo. Recuperarlo y evaluar que el nombre coincida con el esperado.
                        */

                        var disp1 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055, 360, 90);
                        disp1.estadosAnteriores = new List<State>();

                        db.Dispositivos.Add(disp1);
                        var estado_1 = new Apagado(disp1) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-361) };
                        var estado_2 = new Encendido(disp1) { FechaInicial = DateTime.Now.AddHours(-361), FechaFinal = new DateTime(3000, 1, 1) };
                        db.Estados.Add(estado_1);
                        db.Estados.Add(estado_2);

                        db.SaveChanges();

                        var disp = db.Dispositivos.First(u => u.DispositivoID == disp1.DispositivoID);
                        Console.WriteLine("El dispositivo encontrado (ID:{0}) es un {1}\n", disp.DispositivoID, disp.Nombre);

                        var result = db.Estados.Where(e => e.DispositivoID == disp.DispositivoID).ToList();

                        Console.WriteLine("Los intervalos en los que estuvo encendido son:\n");

                        foreach (var e in result)
                        {
                           if(e is Encendido)
                                Console.WriteLine( "Fecha inicial: {0} - Fecha final: {1} \n", e.FechaInicial, e.FechaFinal );
                        }


                        disp.Nombre = "tv LED de 32 pulgadas";
                        Console.WriteLine("Cambiando el nombre del dispositivo a: {0}\n", disp.Nombre);

                        db.SaveChanges();

                        var nombreNuevo = db.Dispositivos.First(d => d.DispositivoID == disp1.DispositivoID);

                        Console.WriteLine("El nuevo nombre del dispositivo recuperado es {0}: \n",nombreNuevo.Nombre);

                        break;

                    case 3:
                        /*Crear una nueva regla. Asociarla a un dispositivo. Agregar condiciones y
                          acciones. Persistirla. Recuperarla y ejecutarla. Modificar alguna condición y
                          persistirla. Recuperarla y evaluar que la condición modificada posea la última
                          modificación.
                        */


                        break;

                    case 4:
                        /* Recuperar todos los transformadores persistidos. Registrar la cantidad.
                           Agregar una instancia de Transformador al JSON de entradas. Ejecutar el
                           método de lectura y persistencia. Evaluar que la cantidad actual sea la anterior
                           + 1.
                        */

                        var lTrafo = db.Transformadores.ToList();

                        break;

                    case 5:
                        /*Dado un hogar y un período, mostrar por consola (interfaz de comandos) el consumo total. 
                         *Dado un dispositivo y un período, mostrar por consola su consumo promedio. 
                         * Dado un transformador y un período, mostrar su consumo promedio. 
                         * Recuperar un dispositivo asociado a un hogar de ese transformador e
                         * incrementar un 1000 % el consumo para ese período. 
                         * Persistir el dispositivo.
                         * Nuevamente mostrar el consumo para ese transformador.
                          */


                        var trafo = new Transformador(232344, 234234);
                        db.Transformadores.Add(trafo);
                        db.SaveChanges();
                        var trafoDb = db.Transformadores.First(t => t.Latitud == 232344);
                        
                        //Disp1

                        var userT5 = new Cliente("Jose", "Piedras", "Medrano 951", "Josesito", "clavoUnPalito123", "44838255", "DNI", "2342343");
                        userT5.TransformadorID = trafoDb.TransformadorID;
                        var disp_1 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055, 360, 90);
                        disp_1.UsuarioID = userT5.UsuarioID;
                        disp_1.estadosAnteriores = new List<State>();

                        db.Dispositivos.Add(disp_1);
                        var estado_disp1_1 = new Apagado(disp_1) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-361) };
                        var estado_disp1_2 = new Encendido(disp_1) { FechaInicial = DateTime.Now.AddHours(-361), FechaFinal = new DateTime(3000, 1, 1) };
                        db.Estados.Add(estado_disp1_1);
                        db.Estados.Add(estado_disp1_2);

                        db.SaveChanges();

                        //Disp2
                        var disp_2 = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613, 360, 90);
                        disp_2.UsuarioID = userT5.UsuarioID;
                        disp_2.estadosAnteriores = new List<State>();
                        var estado_disp2_1 = new Apagado(disp_2) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-400) };
                        var estado_disp2_2 = new Encendido(disp_2) { FechaInicial = DateTime.Now.AddHours(-400), FechaFinal = DateTime.Now.AddHours(-200) };
                        var estado_disp2_3 = new Apagado(disp_2) { FechaInicial = DateTime.Now.AddHours(-200), FechaFinal = new DateTime(3000, 1, 1) };
                        db.Dispositivos.Add(disp_2);
                        db.Estados.Add(estado_disp2_1);
                        db.Estados.Add(estado_disp2_2);
                        db.Estados.Add(estado_disp2_3);

                        db.SaveChanges();

                        var usr5 = db.Usuarios.First(u => u.Username == "Josesito");
                        double consumoTotal=0;

                        foreach (DispositivoInteligente d in db.Dispositivos)
                        {
                            if(d.UsuarioID == usr5.UsuarioID)
                            consumoTotal = consumoTotal + d.ConsumoEnPeriodo(DateTime.Now.AddHours(-350), DateTime.Now.AddHours(-150));
                        }

                        Console.WriteLine("El consumo Total para todos los dispositivos de la casa de Josesito es {0}: \n", consumoTotal);

                        var consumoPromedio = consumoTotal / DateTime.Now.AddHours(-150).Subtract(DateTime.Now.AddHours(-350)).Ticks;
                        Console.WriteLine("El consumo prmedio para un dispositivo de Josesito es {0}: \n", consumoPromedio);


                        var suministroTrafo = trafoDb.EnergiaQueEstaSuministrando(DateTime.Now.AddHours(-350), DateTime.Now.AddHours(-150));
                        var suministroPromedio = suministroTrafo / DateTime.Now.AddHours(-150).Subtract(DateTime.Now.AddHours(-350)).Ticks;
                        Console.WriteLine("El suministro promedio del trafo es {0}: \n", suministroPromedio);


                        var dispEditarConsumo = db.Dispositivos.First(d => d.UsuarioID == usr5.UsuarioID);
                        dispEditarConsumo.KWxHora = dispEditarConsumo.KWxHora * 1000;
                        db.SaveChanges();

                        var suministroNuevoTrafo = trafoDb.EnergiaQueEstaSuministrando(DateTime.Now.AddHours(-350), DateTime.Now.AddHours(-150));
                        var suministroNuevoPromedio = suministroNuevoTrafo / DateTime.Now.AddHours(-150).Subtract(DateTime.Now.AddHours(-350)).Ticks;
                        Console.WriteLine("El nuevo suministro promedio del trafo es {0}: \n", suministroNuevoPromedio);


                        break;
                }

            }
        }
    }

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

        private static DBContext _instancia;

        public static DBContext Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new DBContext();
            }
            return _instancia;
        }

        public void BlanquearConexion()
        {
            _instancia = null;
        }

        public DBContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBDisenio;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            
        }
    }
}