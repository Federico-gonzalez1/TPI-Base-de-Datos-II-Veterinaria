using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatos
    {
        //String rutaClinica = @"Data Source=DESKTOP-CHHDRK8\SQLEXPRESS06;Initial Catalog=Clinica;Integrated Security=True;TrustServerCertificate=True";
        String rutaClinica = @"Data Source=localhost\sqlexpress;Initial Catalog=Clinica;Integrated Security=True;TrustServerCertificate=True";

        public AccesoDatos()
        {

        }

        private SqlConnection ObtenerConexion()
        {
            SqlConnection cn = new SqlConnection(rutaClinica);
            try
            {
                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception("Error de conexión a SQL Server: " + ex.Message);
            }
        }


        private SqlDataAdapter ObtenerAdaptador(String consultaSql, SqlConnection cn)
        {
            SqlDataAdapter adaptador;
            try
            {
                adaptador = new SqlDataAdapter(consultaSql, cn);
                return adaptador;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ObtenerTabla(String NombreTabla, String Sql)
        {
            DataSet ds = new DataSet();
            SqlConnection Conexion = ObtenerConexion();
            SqlDataAdapter adp = ObtenerAdaptador(Sql, Conexion);
            adp.Fill(ds, NombreTabla);
            Conexion.Close();
            return ds.Tables[NombreTabla];
        }

        public DataTable ObtenerTabla(string nombreTabla, string sql, SqlParameter[] parametros)
        {
            DataSet ds = new DataSet();
            SqlConnection conexion = ObtenerConexion();

            SqlCommand cmd = new SqlCommand(sql, conexion);

            if (parametros != null)
                cmd.Parameters.AddRange(parametros);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds, nombreTabla);

            conexion.Close();
            return ds.Tables[nombreTabla];
        }

        public int EjecutarProcedimientoAlmacenado(SqlCommand Comando, String NombreSP)
        {
            int FilasCambiadas;
            SqlConnection Conexion = ObtenerConexion();
            SqlCommand cmd = new SqlCommand();
            cmd = Comando;
            cmd.Connection = Conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = NombreSP;
            FilasCambiadas = cmd.ExecuteNonQuery();
            Conexion.Close();
            return FilasCambiadas;
        }

        public Boolean existe(String consulta)
        {
            //Boolean estado = false;
            //SqlConnection Conexion = ObtenerConexion();
            //SqlCommand cmd = new SqlCommand(consulta, Conexion);
            //SqlDataReader datos = cmd.ExecuteReader();
            //if (datos.Read())
            //{
            //    estado = true;
            //}
            //return estado;
            using (SqlConnection Conexion = ObtenerConexion())  // aca se abre
            {
                if (Conexion == null) return false;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, Conexion))
                    {
                        using (SqlDataReader datos = cmd.ExecuteReader())
                        {
                            return datos.Read();  // si lee algo, existe
                        }
                    }
                }
                catch (Exception)
                {
                    return false;  //aca podria logear el error de alguna forma
                }
            }
        }

    }
}
