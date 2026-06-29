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
    public class NegocioPacientes
    {
        public System.Data.DataTable getTabla()
        {
            Datos.DaoPacientes dao = new Datos.DaoPacientes();
            return dao.getTablaPacientes();
        }

        public DataTable buscarPacientes(string busqueda)
        {
            Datos.DaoPacientes dao = new Datos.DaoPacientes();
            return dao.buscarPacientes(busqueda);
        }

        public bool bajaPaciente(string dni)
        {
            //Validar id existente 
            DaoPacientes dao = new DaoPacientes();
            Paciente pac = new Paciente();
            pac.setDni(dni);
            int op = dao.bajaPaciente(pac);
            if (op == 1)
                return true;
            else
                return false;
        }

        public bool actualizarPaciente(Paciente paciente)
        {
            //Validar id existente 
            DaoPacientes dao = new DaoPacientes();
            int op = dao.actualizarPaciente(paciente);
            if (op == 1)
                return true;
            else
                return false;
        }

        public bool altaPaciente(Paciente paciente)
        {
            int cantFilas = 0;

            DaoPacientes dao = new DaoPacientes();

            if(dao.existePacienteEnBaja(paciente) == true)
            {
                // Si existe pero esta dado de baja, lo actualizo con los nuevos datos
                cantFilas = dao.actualizarPaciente(paciente);
            }
            else
            if (dao.existePaciente(paciente) == false)
            {
                cantFilas = dao.agregarPaciente(paciente);
            }

            if (cantFilas == 1)
                return true;
            else
                return false;
        }
    }
}
