using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP0.Helpers.ORM;

namespace TP0.Helpers.Static
{
    public static class DispositivosTotales
    {//todas las opciones de dispositivos en el sistema
        /*static List<DispositivoEstandar> opcionesDeDispositivosEstandares = new List<DispositivoEstandar>();
        static List<DispositivoInteligente> opcionesDeDispositivosInteligentes = new List<DispositivoInteligente>();
        static DispositivoInteligente aireAcondicionado3500 = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613, 360, 90);
        static DispositivoInteligente aireAcondicionado2200 = new DispositivoInteligente("aire acondicionado de 2200 frigorias", "0012", 1.013, 360, 90);
        static DispositivoEstandar televisor21 = new DispositivoEstandar("televisor de 21 pulgadas", "0013", 0.075, 0, 360, 90);
        static DispositivoEstandar televisor29a34 = new DispositivoEstandar("televisor de 29 a 34 pulgadas", "0014", 0.175, 0, 360, 90);
        static DispositivoEstandar televisorLCD40 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0015", 0.18, 0, 360, 90);
        static DispositivoInteligente televisorLED24 = new DispositivoInteligente("televisor LED de 24 pulgadas", "0016", 0.04, 360, 90);
        static DispositivoInteligente televisorLED32 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0017", 0.055, 360, 90);
        static DispositivoInteligente televisorLED40 = new DispositivoInteligente("televisor LED de 40 pulgadas", "0018", 0.08, 360, 90);
        //static DispositivoInteligente heladeraConFreezer = new DispositivoInteligente("heladera con freezer", "0019", 0.09);
        //static DispositivoInteligente heladeraSinFreezer = new DispositivoInteligente("heladera sin freezer", "0020", 0.075);
        static DispositivoInteligente lavarropasAuto = new DispositivoInteligente("lavarropas automatico de 5kg", "0021", 0.175, 30, 6);
        static DispositivoEstandar lavarropasAutoCalentamiento = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0022", 0.875, 0, 30, 6);
        static DispositivoEstandar lavarropasSemiAuto = new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0023", 0.1275, 0, 30, 6);
        static DispositivoEstandar ventiladorDePie = new DispositivoEstandar("ventilador de pie", "0024", 0.09, 0, 360, 120);
        static DispositivoInteligente ventiladorDeTecho = new DispositivoInteligente("ventilador de techo", "0025", 0.06, 360, 120);
        static DispositivoEstandar planchaAVapor = new DispositivoEstandar("plancha a vapor", "0026", 0.75, 0, 30, 3);
        static DispositivoEstandar microondasConvencional = new DispositivoEstandar("microondas convencional", "0027", 0.64, 0, 15, 3);
        static DispositivoInteligente pcDeEscritorio = new DispositivoInteligente("pc de escritorio", "0028", 0.4, 360, 90);
        static DispositivoInteligente lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0029", 0.04, 360, 90);
        static DispositivoInteligente lamparaHalogena60W = new DispositivoInteligente("lampara halogena de 60 W", "0030", 0.06, 360, 90);
        static DispositivoInteligente lamparaHalogena100W = new DispositivoInteligente("lampara halogena de 100 W", "0031", 0.015, 360, 90);
        static DispositivoInteligente lampara11W = new DispositivoInteligente("lampara de 11 W", "0032", 0.011, 360, 90);
        static DispositivoInteligente lampara15W = new DispositivoInteligente("lampara de 15 W", "0033", 0.015, 360, 90);
        static DispositivoInteligente lampara20W = new DispositivoInteligente("lampara de 20 W", "0034", 0.02, 360, 90);*/

        static List<DispositivoEstatico> opcionesDeDispositivos = new List<DispositivoEstatico>();

        public static void LlenarDisps()
        {
            using (var db = new DBContext())
            {
                opcionesDeDispositivos = db.DispEstaticos.ToList();
            }
        }
        public static List<SelectListItem> GetDispositivos()
        {
            LlenarDisps();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (DispositivoEstatico d in opcionesDeDispositivos)
                lista.Add(new SelectListItem() { Value = d.Codigo, Text = d.Nombre });
            return lista;
        }
        public static bool EsInteligente(string id)
        {
            LlenarDisps();
            return opcionesDeDispositivos.Any(disp => disp.Codigo == id && disp.EsInteligente);
        }
        public static DispositivoInteligente EncontrarDispositivoInteligente(string id)
        {
            DispositivoEstatico Disp = opcionesDeDispositivos.Find(d => d.EsInteligente && d.Codigo == id);
            return new DispositivoInteligente(Disp.Nombre, Disp.Codigo, Disp.KWxHora, Disp.Max, Disp.Min);
        }
        public static DispositivoEstandar EncontrarDispositivoEstandard(string id)
        {
            DispositivoEstatico Disp = opcionesDeDispositivos.Find(d => !d.EsInteligente && d.Codigo == id);
            return new DispositivoEstandar(Disp.Nombre, Disp.Codigo, Disp.KWxHora, 0, Disp.Max, Disp.Min);
        }
        public static void AgregarDispEstaticos(List<DispositivoEstatico> list)
        {
            using (var db = new DBContext())
            {
                foreach(DispositivoEstatico d in list)
                    if (!db.DispEstaticos.Any(disp => disp.Codigo == d.Codigo))
                        db.DispEstaticos.Add(d);

                db.SaveChanges();
            }
        }
    }
}