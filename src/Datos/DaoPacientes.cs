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
    public class DaoPacientes
    {
        AccesoDatos ds = new AccesoDatos();
        

        public DataTable getTablaPacientes()
        {

            DataTable tabla = ds.ObtenerTabla("Pacientes", "Select Dni, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento, Telefono from Pacientes WHERE Estado = 1");
            return tabla;
        }

        public DataTable buscarPacientes(string busqueda)
        {
            String consulta = "SELECT * FROM Pacientes WHERE Dni LIKE @BUSQUEDA OR Nombre LIKE @BUSQUEDA OR Apellido LIKE @BUSQUEDA AND Estado = 1 ";

            SqlParameter[] parametros = { new SqlParameter("@BUSQUEDA", "%" + busqueda + "%") };
            DataTable tabla = ds.ObtenerTabla("Pacientes", consulta, parametros);
            return tabla;
        }

        private void ArmarParametrosBajaPaciente(ref SqlCommand Comando, Paciente paciente)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@DniPaciente", SqlDbType.Int);
            SqlParametros.Value = paciente.getDni();
        }

        public int bajaPaciente(Paciente paciente)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosBajaPaciente(ref comando, paciente);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spBajaPaciente");
        }

        public int actualizarPaciente(Paciente paciente)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosPacienteActualizar(ref comando, paciente);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spActualizarPaciente");
        }

        public int agregarPaciente(Paciente paciente)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosPaciente(ref comando, paciente);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spInsertarPaciente");
        }

        public Boolean existePaciente(Paciente paciente)
        {
            String consulta = "Select * from Pacientes where Dni='" + paciente.getDni() + "'";
            return ds.existe(consulta);
        }

        public Boolean existePacienteEnBaja(Paciente paciente)
        {
            String consulta = "Select * from Pacientes where Dni='" + paciente.getDni() + "' AND Estado=0";
            return ds.existe(consulta);
        }

        private void ArmarParametrosPacienteActualizar(ref SqlCommand Comando, Paciente paciente)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@DNI", SqlDbType.NChar);
            SqlParametros.Value = paciente.getDni();
            SqlParametros = Comando.Parameters.Add("@NOMBRE", SqlDbType.NChar);
            SqlParametros.Value = paciente.getNombre();
            SqlParametros = Comando.Parameters.Add("@APELLIDO", SqlDbType.NChar);
            SqlParametros.Value = paciente.getApellido();
            SqlParametros = Comando.Parameters.Add("@SEXO", SqlDbType.NChar);
            SqlParametros.Value = paciente.getSexo();
            SqlParametros = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar);
            SqlParametros.Value = paciente.getNacionalidad();
            SqlParametros = Comando.Parameters.Add("@FECHANAC", SqlDbType.Date);
            SqlParametros.Value = paciente.getFechaNacimiento();
            SqlParametros = Comando.Parameters.Add("@TEL", SqlDbType.NChar);
            SqlParametros.Value = paciente.getTelefono();
        }

        private void ArmarParametrosPaciente(ref SqlCommand Comando, Paciente paciente)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@DNI", SqlDbType.NChar);
            SqlParametros.Value = paciente.getDni();
            SqlParametros = Comando.Parameters.Add("@NOMBRE", SqlDbType.NChar);
            SqlParametros.Value = paciente.getNombre();
            SqlParametros = Comando.Parameters.Add("@APELLIDO", SqlDbType.NChar);
            SqlParametros.Value = paciente.getApellido();
            SqlParametros = Comando.Parameters.Add("@SEXO", SqlDbType.NChar);
            SqlParametros.Value = paciente.getSexo();
            SqlParametros = Comando.Parameters.Add("@NACIONALIDAD", SqlDbType.NChar);
            SqlParametros.Value = paciente.getNacionalidad();
            SqlParametros = Comando.Parameters.Add("@FECHANAC", SqlDbType.Date);
            SqlParametros.Value = paciente.getFechaNacimiento();
            SqlParametros = Comando.Parameters.Add("@TELEFONO", SqlDbType.NChar);
            SqlParametros.Value = paciente.getTelefono();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Bit);
            SqlParametros.Value = paciente.getEstado();
        }

        
    }
}
