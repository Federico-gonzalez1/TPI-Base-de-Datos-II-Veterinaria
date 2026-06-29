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
    public class DaoHorarioAtencion
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable getTablaHorarios()
        {

            DataTable tabla = ds.ObtenerTabla("HorariosAtencion", "Select * WHERE Estado = 1");
            return tabla;
        }

        public DataTable getHorariosMedico(int legajo, int diaSemana)
        {
            DataTable tabla = ds.ObtenerTabla("HorariosAtencion", "Select * from HorariosAtencion WHERE Legajo_medico='" + legajo + "' AND  DiaSemana = '" + diaSemana +"' AND Estado=1");
            HorarioAtencion horario = new HorarioAtencion();

                return tabla;

        }

        public int agregarHorario(HorarioAtencion horarioAtencion)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosHorarioAtencion(ref comando, horarioAtencion);
            return ds.EjecutarProcedimientoAlmacenado(comando, "spInsertarHorarioAtencion");
        }


        private void ArmarParametrosHorarioAtencion(ref SqlCommand Comando, HorarioAtencion horarioAtencion)
        {
            SqlParameter SqlParametros = new SqlParameter();
            //SqlParametros = Comando.Parameters.Add("@IDHORARIO", SqlDbType.Int);
            //SqlParametros.Value = horarioAtencion.getIdHorario();
            SqlParametros = Comando.Parameters.Add("@DIASEMANA", SqlDbType.TinyInt);
            SqlParametros.Value = horarioAtencion.getDiaSemana();
            SqlParametros = Comando.Parameters.Add("@HORAINICIO", SqlDbType.Time);
            SqlParametros.Value = horarioAtencion.getHoraInicio();
            SqlParametros = Comando.Parameters.Add("@HORAFIN", SqlDbType.Time);
            SqlParametros.Value = horarioAtencion.getHoraFin();
            SqlParametros = Comando.Parameters.Add("@LEGAJO", SqlDbType.Int);
            SqlParametros.Value = horarioAtencion.getLegajo();
            SqlParametros = Comando.Parameters.Add("@ESTADO", SqlDbType.Bit);
            SqlParametros.Value = horarioAtencion.getEstado();

        }
        public Boolean existeHorario(HorarioAtencion horarioAtencion)
        {
            String consulta = "Select * from HorariosAtencion WHERE Id_horario ='" + horarioAtencion.getIdHorario() + "' AND Estado=1";
            return ds.existe(consulta);
        }

        public bool MedicoAtiendeTalDia(int legajo, int diaSemana)
        {
            String consulta = "SELECT * FROM HorariosAtencion WHERE Legajo_medico = " + legajo + " AND DiaSemana = " + diaSemana + " AND Estado = 1";
            //SqlCommand comando = new SqlCommand(consulta);
            //comando.Parameters.AddWithValue("@Legajo_medico", legajo);
            //comando.Parameters.AddWithValue("@DiaSemana", diaSemana);
            return ds.existe(consulta);
        }
    }
}
