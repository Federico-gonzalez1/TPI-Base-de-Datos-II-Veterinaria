using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class NegocioUsuario
    {

        public DataTable getTabla()
        {
            DaoUsuarios dao = new DaoUsuarios();
            return dao.getTablaUsuarios();
        }

        public bool existeUsuario(Usuario usuario)
        {
            DaoUsuarios dao = new DaoUsuarios();
            return dao.existeUsuario(usuario);
        }

        public bool altaUsuario(Usuario usuario)
        {
            int cantFilas = 0;

            DaoUsuarios dao = new DaoUsuarios();

            if (existeUsuario(usuario) == false)
            {
                cantFilas = dao.altaUsuario(usuario);
            }

            if (cantFilas == 1)
                return true;
            else
                return false;
        }
    }
}
