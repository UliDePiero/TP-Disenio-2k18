using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string apellido, string domicilio, string usuario, string contrasenia, string doc, string tipo, string tel)
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
            AccionAutomatica = false;
            FechaDeAlta = DateTime.Now.ToShortDateString();
            this.EsAdmin = true;
        }
        public Administrador (Usuario u)
        {
            this.EsAdmin = true;
        }
        public Administrador()
        {

        }
        public override void AgregarALaBase()
        {
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

        public override int MesesQueLleva()
        {
            return Static.FechasAdmin.diferenciaDeMeses(DateTime.Parse(FechaDeAlta), DateTime.Now);
        }

        public override bool AlgunDispositivoEncendido()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosEncendidos()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosApagados()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosEnAhorro()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosTotales()
        {
            throw new NotImplementedException();
        }

        public override double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            throw new NotImplementedException();
        }

        public override double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            throw new NotImplementedException();
        }

        public override void AgregarDispInteligente(DispositivoInteligente DI)
        {
            throw new NotImplementedException();
        }

        public override void AgregarDispEstandar(DispositivoEstandar DE)
        {
            throw new NotImplementedException();
        }

        public override void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            throw new NotImplementedException();
        }

        public override RecomendacionXDisp[] SolicitarRecomendacion()
        {
            throw new NotImplementedException();
        }

        public override void ActualizarCategoria()
        {
            throw new NotImplementedException();
        }

      
    }
}