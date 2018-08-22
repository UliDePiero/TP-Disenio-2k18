using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class ClientesImportados
    {
        public static List<Cliente> clientes;
        public static List<Cliente> GetClientes()
        {
            if (clientes.Count > 0)
            {
                return clientes;
            }
            else return null;
        }
        public static Cliente filtrarCliente(string id)
        {
            if (clientes.Count > 0)
            {
                return clientes.Find(x => x.usuario == id);
            }
            else return null;
        }
    }
}
