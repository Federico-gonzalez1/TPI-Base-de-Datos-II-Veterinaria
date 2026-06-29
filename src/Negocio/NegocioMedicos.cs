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
    public class NegocioMedicos
    {
        public DataTable getTabla()
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.getTablaMedicos();
        }

        public DataTable getMedicosEspecialidad(int idEspecialidad)
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.getMedicosEspecialidad(idEspecialidad);
        }

        public bool existeMedico(Medico medico)
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.existeMedico(medico);
        }

        public bool bajaMedico(int legajo)
        {

            DaoMedicos dao = new DaoMedicos();
            Medico med= new Medico();
            med.setLegajo(legajo);
            int op = dao.bajaMedico(med);
            if (op == 1)
                return true;
            else
                return false;
        }

        public bool altaMedico(Medico medico)
        {
            int cantFilas = 0;

            DaoMedicos dao = new DaoMedicos();

            if (existeMedico(medico) == false)
            {
                cantFilas = dao.altaMedico(medico);
            }

            if (cantFilas == 1)
                return true;
            else
                return false;
        }

        public Medico getMedico(string dni)
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.getMedico(dni);
        }

        public Medico getMedicoXLegajo(int legajo)
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.getMedicoXLegajo(legajo);
        }

        public bool actualizarMedico(Medico medico)
        {

            DaoMedicos dao = new DaoMedicos();
            int op = dao.actualizarMedico(medico);
            if (op == 1)
                return true;
            else
                return false;
        }
    }
}
