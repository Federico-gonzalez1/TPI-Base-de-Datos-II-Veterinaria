using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class NegocioHorarioAtencion
    {

        public DataTable getTabla()
        {
            DaoHorarioAtencion dao = new DaoHorarioAtencion();
            return dao.getTablaHorarios();
        }

        public DataTable getHorariosMedico(int legajo, int diaSemana)
        {
            DaoHorarioAtencion dao = new DaoHorarioAtencion();
            return dao.getHorariosMedico(legajo, diaSemana);
        }

        public bool agregarHorarioAtencion(HorarioAtencion horarioAtencion)
        {
            int cantFilas = 0;

            DaoHorarioAtencion dao = new DaoHorarioAtencion();

            if (dao.existeHorario(horarioAtencion) == false)
            {
                cantFilas = dao.agregarHorario(horarioAtencion);
            }

            if (cantFilas == 1)
                return true;
            else
                return false;
        }

        public bool MedicoAtiendeTalDia(int legajo, int diaSemana)
        {
            DaoHorarioAtencion dao = new DaoHorarioAtencion();
            return dao.MedicoAtiendeTalDia(legajo, diaSemana);
        }


    }
}
