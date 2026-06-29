using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DaoMedicos
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable getTablaMedicos()
        {

            DataTable tabla = ds.ObtenerTabla("Medicos", "Select m.Legajo, m.dni_medico, m.Nombre, m.Apellido, m.Sexo, m.Nacionalidad, m.Fecha_Nacimiento, m.Telefono, m.IdEspecialidad, e.Descripcion as Especialidad FROM Medicos m INNER JOIN Especialidades e ON m.IdEspecialidad = e.Id_Especialidad WHERE m.Estado = 1");
            return tabla;
        }

        public Medico getMedico(string dni)
        {
            DataTable tabla = ds.ObtenerTabla("Medicos", "Select * from Medicos where dni_medico='" + dni + "' AND Estado=1");
            Medico medico = new Medico();
            if (tabla.Rows.Count > 0)
            {
                medico.setLegajo(Convert.ToInt32(tabla.Rows[0]["Legajo"]));
                medico.setDni(tabla.Rows[0]["dni_medico"].ToString());
                medico.setNombre(tabla.Rows[0]["Nombre"].ToString());
                medico.setApellido(tabla.Rows[0]["Apellido"].ToString());
                medico.setSexo(tabla.Rows[0]["Sexo"].ToString());
                medico.setNacionalidad(tabla.Rows[0]["Nacionalidad"].ToString());
                medico.setFechaNacimiento(Convert.ToDateTime(tabla.Rows[0]["Fecha_Nacimiento"]));
                medico.setTelefono(tabla.Rows[0]["Telefono"].ToString());
                medico.setIdEspecialidad(Convert.ToInt32(tabla.Rows[0]["IdEspecialidad"]));
                medico.setEstado(Convert.ToBoolean(tabla.Rows[0]["Estado"]));
            }
            return medico;
        }

        public Medico getMedicoXLegajo(int legajo)
        {
            DataTable tabla = ds.ObtenerTabla("Medicos", "Select * from Medicos where Legajo='" + legajo + "' AND Estado=1");
            Medico medico = new Medico();
            if (tabla.Rows.Count > 0)
            {
                medico.setLegajo(Convert.ToInt32(tabla.Rows[0]["Legajo"]));
                medico.setDni(tabla.Rows[0]["dni_medico"].ToString());
                medico.setNombre(tabla.Rows[0]["Nombre"].ToString());
                medico.setApellido(tabla.Rows[0]["Apellido"].ToString());
                medico.setSexo(tabla.Rows[0]["Sexo"].ToString());
                medico.setNacionalidad(tabla.Rows[0]["Nacionalidad"].ToString());
                medico.setFechaNacimiento(Convert.ToDateTime(tabla.Rows[0]["Fecha_Nacimiento"]));
                medico.setTelefono(tabla.Rows[0]["Telefono"].ToString());
                medico.setIdEspecialidad(Convert.ToInt32(tabla.Rows[0]["IdEspecialidad"]));
                medico.setEstado(Convert.ToBoolean(tabla.Rows[0]["Estado"]));
            }
            return medico;
        }

        public DataTable getMedicosEspecialidad(int idEspecialidad)
        {
            DataTable tabla = ds.ObtenerTabla("Medicos", "Select m.Legajo, m.Nombre, m.Apellido FROM Medicos m  WHERE m.Estado = 1 AND m.IdEspecialidad = " + idEspecialidad);

                return tabla;    
            
        }
        public Boolean existeMedico(Medico medico)
        {
            String consulta = "Select * from Medicos where dni_medico='" + medico.getDni() + "' AND Estado=1";
            return ds.existe(consulta);
        }

        private void ArmarParametrosBajaMedico(ref SqlCommand Comando, Medico med)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@Legajomedico", SqlDbType.Int);
            SqlParametros.Value = med.getLegajo();
        }

        public int bajaMedico(Medico med)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosBajaMedico(ref comando, med);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spBajaMedico");
        }

        public int altaMedico(Medico med)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosAltaMedico(ref comando, med);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spInsertarMedico");
        }



        public int actualizarMedico(Medico medico)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosMedico(ref comando, medico);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spActualizarMedico");
        }
        private void ArmarParametrosAltaMedico(ref SqlCommand Comando, Medico medico)
        {
            SqlParameter SqlParametros = new SqlParameter();

            Comando.Parameters.Clear();
            SqlParametros = Comando.Parameters.Add("@DNI", SqlDbType.NChar);
            SqlParametros.Value = medico.getDni();
            SqlParametros = Comando.Parameters.Add("@NOMBRE", SqlDbType.NChar);
            SqlParametros.Value = medico.getNombre();
            SqlParametros = Comando.Parameters.Add("@APELLIDO", SqlDbType.NChar);
            SqlParametros.Value = medico.getApellido();
            SqlParametros = Comando.Parameters.Add("@SEXO", SqlDbType.NChar);
            SqlParametros.Value = medico.getSexo();
            SqlParametros = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar);
            //SqlParametros.Value = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar, 50).Value = DBNull.Value;
            //SqlParametros.Value = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar, 50).Value = medico.getNacionalidad() ?? (object)DBNull.Value;
            SqlParametros.Value = medico.getNacionalidad();
            //SqlParametros = Comando.Parameters.Add("@FECHANACIMIENTO", SqlDbType.Date);
            //SqlParametros.Value = medico.getFechaNacimiento();
            Comando.Parameters.Add("@FECHANACIMIENTO", SqlDbType.Date).Value = medico.getFechaNacimiento();
            SqlParametros = Comando.Parameters.Add("@TELEFONO", SqlDbType.NChar);
            SqlParametros.Value = medico.getTelefono();
            SqlParametros = Comando.Parameters.Add("@IDESPECIALIDAD", SqlDbType.Int);
            SqlParametros.Value = medico.getIdEspecialidad();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Bit);
            SqlParametros.Value = medico.getEstado();

        }

        private void ArmarParametrosMedico(ref SqlCommand Comando, Medico medico)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@LEGAJOMEDICO", SqlDbType.Int);
            SqlParametros.Value = medico.getLegajo();
            SqlParametros = Comando.Parameters.Add("@NOMBRE", SqlDbType.NChar);
            SqlParametros.Value = medico.getNombre();
            SqlParametros = Comando.Parameters.Add("@APELLIDO", SqlDbType.NChar);
            SqlParametros.Value = medico.getApellido();
            SqlParametros = Comando.Parameters.Add("@SEXO", SqlDbType.NChar);
            SqlParametros.Value = medico.getSexo();
            SqlParametros = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar);
            SqlParametros.Value = medico.getNacionalidad();
            SqlParametros = Comando.Parameters.Add("@FECHANAC", SqlDbType.Date);
            SqlParametros.Value = medico.getFechaNacimiento();
            SqlParametros = Comando.Parameters.Add("@TEL", SqlDbType.NChar);
            SqlParametros.Value = medico.getTelefono();
            SqlParametros = Comando.Parameters.Add("@ESPEC", SqlDbType.Int);
            SqlParametros.Value = medico.getIdEspecialidad();
        }

    }
}
