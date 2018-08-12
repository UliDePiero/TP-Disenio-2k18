using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class DispositivosTotales
    {
        static List<DispositivoEstandar> opcionesDeDispositivosEstandares = new List<DispositivoEstandar>();
        static List<DispositivoInteligente> opcionesDeDispositivosInteligentes = new List<DispositivoInteligente>();
        static DispositivoInteligente aireAcondicionado3500 = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613,360,90);
        static DispositivoInteligente aireAcondicionado2200 = new DispositivoInteligente("aire acondicionado de 2200 frigorias", "0012", 1.013, 360, 90);
        static DispositivoEstandar televisor21 = new DispositivoEstandar("televisor de 21 pulgadas", "0012", 0.075, 0,360,90);
        static DispositivoEstandar televisor29a34 = new DispositivoEstandar("televisor de 29 a 34 pulgadas", "0013", 0.175, 0, 360, 90);
        static DispositivoEstandar televisorLCD40 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
        static DispositivoInteligente televisorLED24 = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04, 360, 90);
        static DispositivoInteligente televisorLED32 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055, 360, 90);
        static DispositivoInteligente televisorLED40 = new DispositivoInteligente("televisor LED de 40 pulgadas", "0017", 0.08, 360, 90);
        static DispositivoInteligente heladeraConFreezer = new DispositivoInteligente("heladera con freezer", "0018", 0.09);
        static DispositivoInteligente heladeraSinFreezer = new DispositivoInteligente("heladera sin freezer", "0019", 0.075);
        static DispositivoInteligente lavarropasAuto = new DispositivoInteligente("lavarropas automatico de 5kg", "0020", 0.175,30,6);
        static DispositivoEstandar lavarropasAutoCalentamiento = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6);
        static DispositivoEstandar lavarropasSemiAuto = new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0022", 0.1275, 0, 30, 6);
        static DispositivoEstandar ventiladorDePie = new DispositivoEstandar("ventilador de pie", "0023", 0.09, 0,360,120);
        static DispositivoInteligente ventiladorDeTecho = new DispositivoInteligente("ventilador de techo", "0024", 0.06,360, 120);
        static DispositivoEstandar planchaAVapor = new DispositivoEstandar("plancha a vapor", "0011", 0.75, 0,30,3);
        static DispositivoEstandar microondasConvencional = new DispositivoEstandar("microondas convencional", "0011", 0.64, 0,15,3);
        static DispositivoInteligente pcDeEscritorio = new DispositivoInteligente("pc de escritorio", "0011", 0.4,360,90);
        static DispositivoInteligente lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
        static DispositivoInteligente lamparaHalogena60W = new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90);
        static DispositivoInteligente lamparaHalogena100W = new DispositivoInteligente("lampara halogena de 100 W", "0011", 0.015, 360, 90);
        static DispositivoInteligente lampara11W = new DispositivoInteligente("lampara de 11 W", "0011", 0.011, 360, 90);
        static DispositivoInteligente lampara15W = new DispositivoInteligente("lampara de 15 W", "0011", 0.015, 360, 90);
        static DispositivoInteligente lampara20W = new DispositivoInteligente("lampara de 20 W", "0011", 0.02, 360, 90);
        public static void llenarListas()
        {
            if (opcionesDeDispositivosEstandares.Count() == 0)
            {
                opcionesDeDispositivosEstandares.Add(televisor21);
                opcionesDeDispositivosEstandares.Add(televisor29a34);
                opcionesDeDispositivosEstandares.Add(televisorLCD40);
                opcionesDeDispositivosEstandares.Add(lavarropasAutoCalentamiento);
                opcionesDeDispositivosEstandares.Add(lavarropasSemiAuto);
                opcionesDeDispositivosEstandares.Add(ventiladorDePie);
                opcionesDeDispositivosEstandares.Add(planchaAVapor);
                opcionesDeDispositivosEstandares.Add(microondasConvencional);
            }
            if(opcionesDeDispositivosInteligentes.Count() ==0)
            {
                opcionesDeDispositivosInteligentes.Add(aireAcondicionado2200);
                opcionesDeDispositivosInteligentes.Add(aireAcondicionado3500);
                opcionesDeDispositivosInteligentes.Add(televisorLED24);
                opcionesDeDispositivosInteligentes.Add(televisorLED32);
                opcionesDeDispositivosInteligentes.Add(televisorLED40);
                opcionesDeDispositivosInteligentes.Add(heladeraConFreezer);
                opcionesDeDispositivosInteligentes.Add(heladeraSinFreezer);
                opcionesDeDispositivosInteligentes.Add(lavarropasAuto);
                opcionesDeDispositivosInteligentes.Add(ventiladorDeTecho);
                opcionesDeDispositivosInteligentes.Add(pcDeEscritorio);
                opcionesDeDispositivosInteligentes.Add(lampara11W);
                opcionesDeDispositivosInteligentes.Add(lamparaHalogena100W);
                opcionesDeDispositivosInteligentes.Add(lamparaHalogena40W);
                opcionesDeDispositivosInteligentes.Add(lamparaHalogena60W);
                opcionesDeDispositivosInteligentes.Add(lampara15W);
                opcionesDeDispositivosInteligentes.Add(lampara20W);
            }
        }
        public static List<DispositivoEstandar> GetDispositivoEstandars()
        {
            llenarListas();
            return opcionesDeDispositivosEstandares;
        }
        public static List<DispositivoInteligente> GetDispositivoInteligentes()
        {
            llenarListas();
            return opcionesDeDispositivosInteligentes;
        }
    }
}
