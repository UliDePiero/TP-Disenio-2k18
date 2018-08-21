using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Zona
    {
        public List<Transformador> transformadores;
        public double latitud;
        public double longitud;
        public double radio;
        double energia;
        public double consumoTotal(DateTime fInicial, DateTime fFinal)
        {
		    foreach (Transformador trafo in transformadores)
		    {
		    energia += trafo.energiaQueEstaSuministrando(fInicial, fFinal);
		    }
	    return energia;
	    }
    }
}