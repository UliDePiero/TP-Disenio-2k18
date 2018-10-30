using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers.Static
{
    public static class ClientesImportados
    {
        public static List<Cliente> clientes;
        public static List<Cliente> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var db = new DBContext())
            {
                foreach (var u in db.Usuarios)
                    if (!u.EsAdmin)
                        clientes.Add(new Cliente(u.Nombre, u.Apellido, u.Domicilio, u.Username, u.Contrasenia, u.Documento, u.TipoDocumento, u.Telefono)
                        { UsuarioID = u.UsuarioID});
            }
            return clientes;
        }
        public static Cliente filtrarCliente(string id)
        {
            if (clientes.Count > 0)
            {
                return clientes.Find(x => x.Username == id);
            }
            else return null;
        }
    }
}
