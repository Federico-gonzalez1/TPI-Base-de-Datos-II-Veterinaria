using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Turno
    {
        int _Id_turno;
        DateTime _Fecha;
        TimeSpan _Hora;
        string _dni_Paciente;
        int _Legajo_Medico;
        int _Estado;
        string _observaciones;

        public Turno()
        { }
        public Turno(DateTime fecha, TimeSpan hora, string dni_Paciente, int legajo_Medico, int estado)
        {
            _Fecha = fecha;
            _Hora = hora;
            _dni_Paciente = dni_Paciente;
            _Legajo_Medico = legajo_Medico;
            _Estado = estado;
        }

        public Turno(int idturno, int estado,  string observaciones)
        {
            _Id_turno = idturno;
            _Estado = estado;
            _observaciones = observaciones;
        }

        public int getId_turno()
        {
            return _Id_turno;
        }

        public void setId_turno(int id_turno)
        {
            _Id_turno = id_turno;
        }

        public DateTime getFecha()
        {
            return _Fecha;
        }

        public void setFecha(DateTime fecha)
        {
            _Fecha = fecha;
        }

        public TimeSpan getHora()
        {
            return _Hora;
        }

        public void setHora(TimeSpan hora)
        {
            _Hora = hora;
        }

        public string getDni_Paciente()
        {
            return _dni_Paciente;
        }

        public void setDni_Paciente(string dni_Paciente)
        {
            _dni_Paciente = dni_Paciente;
        }

        public int getLegajo_Medico()
        {
            return _Legajo_Medico;
        }

        public void setLegajo_Medico(int legajo_Medico)
        {
            _Legajo_Medico = legajo_Medico;
        }

        public int getEstado()
        {
            return _Estado;
        }

        public void setEstado(int estado)
        {
            _Estado = estado;
        }

        public string getObservaciones()
        {
            return _observaciones;
        }

        public void setObservaciones(string observaciones)
        {
            _observaciones = observaciones;
        }

    }
}
