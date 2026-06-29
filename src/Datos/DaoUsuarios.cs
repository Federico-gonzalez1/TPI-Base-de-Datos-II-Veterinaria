using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoUsuarios
    {
        AccesoDatos ds = new AccesoDatos();


        public DataTable getTablaUsuarios()
        {

            DataTable tabla = ds.ObtenerTabla("Usuarios", "Select * from Usuarios");
            return tabla;
        }

        public Boolean existeUsuario(Usuario usuario)
        {
            String consulta = "Select * from Usuarios where Usuario='" + usuario.getNombreUsuario() + "'";
            return ds.existe(consulta);
        }

        public int altaUsuario(Usuario usuario)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosUsuario(ref comando, usuario);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spInsertarUsuario");
        }
        // Falta implementar ArmarParametrosUsuario y terminar AltaUsuario.
        //Despues en GestionMedicos agregar el metodo para dar de alta un usuario.

        private void ArmarParametrosUsuario(ref SqlCommand Comando, Usuario usuario)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@USUARIO", SqlDbType.Char);
            SqlParametros.Value = usuario.getNombreUsuario();
            SqlParametros = Comando.Parameters.Add("@CONTRA", SqlDbType.Char);
            SqlParametros.Value = usuario.getContrasenia();
            SqlParametros = Comando.Parameters.Add("@ESADMIN", SqlDbType.Bit);
            SqlParametros.Value = usuario.getEsAdmin();
            SqlParametros = Comando.Parameters.Add("@LEGAJOMEDICO", SqlDbType.Int);
            SqlParametros.Value = usuario.getLegajoMedico();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Bit);
            SqlParametros.Value = usuario.getEstado();

        }
    }
}
