using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Persona
    {
        public int id;
        FechasAdmin f = new FechasAdmin();
        public int MesesQueLleva()
        {
            return f.DiferenciaDeMeses(fechaDeAlta);
        }
    }
}