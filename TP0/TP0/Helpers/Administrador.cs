using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Persona
    {
        FechasAdmin f = new FechasAdmin();
        public int id;
        public int mesesQueLleva()
        {
            int meses = f.diferenciaDeMeses(fechaDeAlta);
            return meses;
        }
    }
}