using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Transformador
    {
        public Zona zonaGeografica;
        public List<Cliente> clientes;
        double energiaTotal;
        public double energiaQueEstaSuministrando(DateTime fInicial, DateTime fFinal)
        {
            foreach (Cliente cli in clientes)
		    {
		    energiaTotal += cli.KwTotales(fInicial, fFinal);
		    }
	        return energiaTotal;
	    }
    }
}