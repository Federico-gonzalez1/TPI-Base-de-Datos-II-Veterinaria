using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class HorarioAtencion
    {
        int _idHorario;
        int _diaSemana;
        TimeSpan _horaInicio;
        TimeSpan _horaFin;
        int _legajoMedico;
        bool _estado;

        public HorarioAtencion()
        { }

        public HorarioAtencion(int idHorario, int diaSemana, TimeSpan horaInicio, TimeSpan horaFin, int legajoMedico, bool estado)
        {
            _idHorario = idHorario;
            _diaSemana = diaSemana;
            _horaInicio = horaInicio;
            _horaFin = horaFin;
            _legajoMedico = legajoMedico;
            _estado = estado;
        }

        public HorarioAtencion(int legajoMedico, int diaSemana, TimeSpan horaInicio, TimeSpan horaFin, bool estado)
        {
            _legajoMedico = legajoMedico;
            _diaSemana = diaSemana;
            _horaInicio = horaInicio;
            _horaFin = horaFin;
            _estado = estado;
        }

        public int getIdHorario()
        {
            return _idHorario;
        }

        public int getDiaSemana()
        {
            return _diaSemana;
        }

        public TimeSpan getHoraInicio()
        {
            return _horaInicio;
        }

        public TimeSpan getHoraFin()
        {
            return _horaFin;
        }

        public int getLegajo()
        {
            return _legajoMedico;
        }

        public bool getEstado()
        {
            return _estado;
        }

        public void setHorario(int idHorario)
        {
            _idHorario = idHorario;
        }

        public void setDiaSemana(int diaSemana)
        {
            _diaSemana = diaSemana;
        }

        public void setHoraInicio(TimeSpan horaInicio)
        {
            _horaInicio = horaInicio;
        }

        public void setHoraFin(TimeSpan horaFin)
        {
            _horaFin = horaFin;
        }

        public void setLegajo(int legajoMedico)
        {
            _legajoMedico = legajoMedico;
        }

        public void setEstado(bool estado)
        {
            _estado = estado;
        }


    }
}
