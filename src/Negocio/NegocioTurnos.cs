using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class NegocioTurnos
    {
        DaoTurnos dao = new DaoTurnos();

        public DataTable getTurnosMedicoFecha(int legajoMedico, DateTime fecha)
        {
            return dao.getTurnosMedicoFecha(legajoMedico, fecha);
        }

        public DataTable getTablaTurnos()
        {
            return dao.getTablaTurnos();
        }

        public DataTable getTurnosAusentesXDia(int diaSemana)
        {
            return dao.getTurnosAusentesXDia(diaSemana);
        }

        public DataTable getTotalTurnosXDia(int diaSemana)
        {
            return dao.getTotalTurnosXDia(diaSemana);
        }

        public DataTable getTurnosXLegajoMedico(int legajo)
        {
            return dao.getTurnosXLegajoMedico(legajo);
        }

        public DataTable buscarTurnosFiltrados(DateTime? desde, DateTime? hasta, string idEstado)
        {
            DataTable tabla = dao.buscarTurnosFiltrados(desde, hasta, idEstado);
            return tabla;
        }

        public bool actualizarTurno(Turno turno)
        {

            DaoTurnos dao = new DaoTurnos();
            int op = dao.actualizarTurno(turno);
            if (op == 1)
                return true;
            else
                return false;
        }

        public DataTable buscarTurnoXPaciente(string busqueda)
        {
            DataTable tabla = dao.buscarTurnoXPaciente(busqueda);
            return tabla;
        }

        public bool agregarTurno(Turno turno)
        {
            int cantFilas = 0;
            cantFilas = dao.agregarTurno(turno);
            if (cantFilas == 1)
                return true;
            else
                return false;
        }
    }
}
