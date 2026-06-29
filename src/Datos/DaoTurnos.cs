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
    public class DaoTurnos
    {
        AccesoDatos ds = new AccesoDatos();
        public DataTable getTurnosMedicoFecha(int legajoMedico, DateTime fecha)
        {
            //DataTable tabla = ds.ObtenerTabla("Turnos", "Select * from Turnos WHERE Legajo_Medico=@LEGAJOMEDICO AND Fecha=@FECHA AND Estado=1");
            DataTable tabla = ds.ObtenerTabla("Turnos", $"SELECT * FROM Turnos WHERE Legajo_Medico = { legajoMedico } AND Fecha = '{fecha:yyyy-MM-dd}' AND Estado = 1");
            HorarioAtencion horario = new HorarioAtencion();

            return tabla;

        }

        public DataTable getTablaTurnos()
        {
            DataTable tabla = ds.ObtenerTabla("Turnos", "Select t.Id_Turno, t.Fecha, t.Hora, t.Legajo_Medico, t.Observaciones, t.dni_Paciente, p.Apellido + ', ' + p.Nombre AS NombreCompletoPaciente, et.Descripcion AS DescripcionEstado, t.Estado from Turnos t INNER JOIN Pacientes p ON t.dni_Paciente = p.Dni  INNER JOIN EstadosTurno et ON t.Estado = et.IdEstado ORDER BY t.Fecha DESC, t.Hora");
            return tabla;
        }

        public DataTable getTurnosAusentesXDia(int diaSemana)
        {
            SqlParameter[] parametro = { new SqlParameter("@DIASEMANA", diaSemana) };
            DataTable tabla = ds.ObtenerTabla("Turnos", "Select t.Id_Turno, t.Fecha, t.Hora, t.Legajo_Medico, t.Observaciones, t.dni_Paciente, p.Apellido + ', ' + p.Nombre AS NombreCompletoPaciente, et.Descripcion AS DescripcionEstado, t.Estado from Turnos t INNER JOIN Pacientes p ON t.dni_Paciente = p.Dni  INNER JOIN EstadosTurno et ON t.Estado = et.IdEstado WHERE t.Estado = 3 AND DATEPART(weekday, t.Fecha) = @DIASEMANA ORDER BY t.Fecha DESC, t.Hora", parametro);
            return tabla;

        }

        public DataTable getTotalTurnosXDia(int diaSemana)
        {
            SqlParameter[] parametro = { new SqlParameter("@DIASEMANA", diaSemana) };
            DataTable tabla = ds.ObtenerTabla("Turnos", "SELECT COUNT(*) FROM Turnos WHERE DATEPART(weekday, Fecha) = @DIASEMANA", parametro);
            return tabla;

        }

        public DataTable getTurnosXLegajoMedico(int legajo)
        {
            DataTable tabla = ds.ObtenerTabla("Turnos", 
                "Select t.Id_Turno, t.Fecha, t.Hora, t.Legajo_Medico, t.Observaciones, t.dni_Paciente, p.Apellido + ', ' + p.Nombre AS NombreCompletoPaciente, et.Descripcion AS DescripcionEstado, t.Estado " +
                "from Turnos t INNER JOIN Pacientes p ON t.dni_Paciente = p.Dni " +
                "INNER JOIN EstadosTurno et ON t.Estado = et.IdEstado " +
                "WHERE t.Legajo_Medico='" + legajo + "' " +
                "ORDER BY t.Fecha DESC, t.Hora");
                return tabla;
        }

        public DataTable buscarTurnoXPaciente(string busqueda)
        {
            String consulta = "SELECT t.Id_Turno, t.Fecha, t.Hora, t.Legajo_Medico, t.Observaciones, p.Apellido + ', ' + p.Nombre AS NombreCompletoPaciente, et.Descripcion AS DescripcionEstado, t.Estado FROM Turnos t INNER JOIN Pacientes p ON t.dni_Paciente = p.Dni INNER JOIN EstadosTurno et ON t.Estado = et.IdEstado WHERE p.Nombre LIKE @BUSQUEDA OR p.Apellido LIKE @BUSQUEDA ORDER BY t.Fecha DESC, t.Hora";

            SqlParameter[] parametros = { new SqlParameter("@BUSQUEDA", "%" + busqueda + "%") };
            DataTable tabla = ds.ObtenerTabla("Turnos", consulta, parametros);
            return tabla;
        }

        public DataTable buscarTurnosFiltrados(DateTime? desde, DateTime? hasta, string idEstado)
        {
            string consulta = "SELECT t.Id_Turno, t.Fecha, t.Hora, t.Legajo_Medico, t.Observaciones, p.Apellido + ', ' + p.Nombre AS NombreCompletoPaciente, et.Descripcion AS DescripcionEstado, t.Estado FROM Turnos t INNER JOIN Pacientes p ON t.dni_Paciente = p.Dni INNER JOIN EstadosTurno et ON t.Estado = et.IdEstado WHERE 1=1";

            SqlParameter[] parametros = new SqlParameter[3];
            int i = 0;

            if (desde.HasValue)
            {
                parametros[i++] = new SqlParameter("@DESDE", desde.Value);
                consulta += " AND t.Fecha >= @DESDE";

            }
            if(hasta.HasValue)
            {
                parametros[i++] = new SqlParameter("@HASTA", hasta.Value);
                consulta += " AND t.Fecha <= @HASTA";
            }
            if(int.TryParse(idEstado, out int estadoSeleccionado) && estadoSeleccionado != 0)
            {
                parametros[i++] = new SqlParameter("@IDESTADO", estadoSeleccionado);
                consulta += " AND t.Estado = @IDESTADO";
            }

            consulta += " ORDER BY t.Fecha DESC, t.Hora";


            //SqlParameter[] parametros = 
            //{ 
            //    new SqlParameter("@DESDE", (object)desde ?? DBNull.Value),
            //    new SqlParameter("@HASTA", (object)hasta ?? DBNull.Value),
            //    new SqlParameter("@IDESTADO", (object)estadoSeleccionado ?? DBNull.Value)
            //};
            Array.Resize(ref parametros, i);

            DataTable tabla = ds.ObtenerTabla("Turnos", consulta, parametros);
            return tabla;
        }

        public int actualizarTurno(Turno turno)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosActualizarTurno(ref comando, turno);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spActualizarTurno");
        }

        private void ArmarParametrosActualizarTurno(ref SqlCommand Comando, Turno turno)
        {
            SqlParameter SqlParametros = new SqlParameter();

            SqlParametros = Comando.Parameters.Add("@IDTURNO", SqlDbType.Int);
            SqlParametros.Value = turno.getId_turno();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Int);
            SqlParametros.Value = turno.getEstado();
            SqlParametros = Comando.Parameters.Add("@OBSERVACIONES", SqlDbType.VarChar);
            SqlParametros.Value = string.IsNullOrEmpty(turno.getObservaciones()) ? (object)DBNull.Value : turno.getObservaciones();

        }

        private void ArmarParametrosAltaTurno(ref SqlCommand Comando, Turno turno)
        {
            SqlParameter SqlParametros = new SqlParameter();

            SqlParametros = Comando.Parameters.Add("@LEGAJOMEDICO", SqlDbType.Int);
            SqlParametros.Value = turno.getLegajo_Medico();
            SqlParametros = Comando.Parameters.Add("@FECHA", SqlDbType.Date);
            SqlParametros.Value = turno.getFecha();

        }

        public int agregarTurno(Turno turno)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosTurnos(ref comando, turno);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spInsertarTurno");
        }


        //public DataTable getTurnosMedicoFecha(int legajoMedico, DateTime fecha)
        //{
        //    string sql = "Select * from Turnos WHERE Legajo_Medico=@legajoMedico AND Fecha=@fecha AND Estado=1";
        //    SqlConnection conn = ds.ObtenerConexion();
        //    SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
        //    adp.SelectCommand.Parameters.AddWithValue("@legajoMedico", legajoMedico);
        //    adp.SelectCommand.Parameters.AddWithValue("@fecha", fecha.Date);
        //    DataSet dsResult = new DataSet();
        //    adp.Fill(dsResult, "Turnos");
        //    conn.Close();
        //    return dsResult.Tables["Turnos"];
        //}

        private void ArmarParametrosTurnos(ref SqlCommand Comando, Turno turno)
        {
            SqlParameter SqlParametros = new SqlParameter();

            SqlParametros = Comando.Parameters.Add("@FECHA", SqlDbType.Date);
            SqlParametros.Value = turno.getFecha();
            SqlParametros = Comando.Parameters.Add("@HORA", SqlDbType.Time);
            SqlParametros.Value = turno.getHora();
            SqlParametros = Comando.Parameters.Add("@DNI", SqlDbType.NChar);
            SqlParametros.Value = turno.getDni_Paciente();
            SqlParametros = Comando.Parameters.Add("@LEGAJOMEDICO", SqlDbType.Int);
            SqlParametros.Value = turno.getLegajo_Medico();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Bit);
            SqlParametros.Value = turno.getEstado();

        }
    }
}
