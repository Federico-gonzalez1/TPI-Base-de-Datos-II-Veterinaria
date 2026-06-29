using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico
    {
        int _legajo;
        string _dni;
        string _nombre;
        string _apellido;
        string _sexo;
        string _nacionalidad;
        DateTime _fechaNacimiento;
        string _telefono;
        int _idEspecialidad;
        bool _estado;

        public Medico()
        { }

        public Medico(int legajo, string dni, string nombre, string apellido, string sexo, string nacionalidad, DateTime fechaNacimiento, string telefono, int idEspecialidad)
        {
            _legajo = legajo;
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _sexo = sexo;
            _nacionalidad = nacionalidad;
            _fechaNacimiento = fechaNacimiento;
            _telefono = telefono;
            _idEspecialidad = idEspecialidad;
        }

        public Medico(string dni, string nombre, string apellido, string sexo, string nacionalidad, DateTime fechaNacimiento, string telefono, int idEspecialidad, bool estado)
        {
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _sexo = sexo;
            _nacionalidad = nacionalidad;
            _fechaNacimiento = fechaNacimiento;
            _telefono = telefono;
            _idEspecialidad = idEspecialidad;
            _estado = estado;
        }

        public Medico(Medico medico)
        {
            _legajo = medico.getLegajo();
            _dni = medico.getDni();
            _nombre = medico.getNombre();
            _apellido = medico.getApellido();
            _sexo = medico.getSexo();
            _nacionalidad = medico.getNacionalidad();
            _fechaNacimiento = medico.getFechaNacimiento();
            _telefono = medico.getTelefono();
            _idEspecialidad = medico.getIdEspecialidad();
            _estado = medico.getEstado();
        }

        public Medico(int legajo)
        {
            _legajo = legajo;
        }

        public int getLegajo()
        {
            return _legajo;
        }

        public void setLegajo(int legajo)
        {
            _legajo = legajo;
        }

        public string getDni()
        {
            return _dni;
        }

        public void setDni(string dni)
        {
            _dni = dni;
        }

        public string getNombre()
        {
            return _nombre;
        }

        public void setNombre(string nombre)
        {
            _nombre = nombre;
        }

        public string getApellido()
        {
            return _apellido;
        }

        public void setApellido(string apellido)
        {
            _apellido = apellido;
        }

        public string getSexo()
        {
            return _sexo;
        }

        public void setSexo(string sexo)
        {
            _sexo = sexo;
        }

        public string getNacionalidad()
        {
            return _nacionalidad;
        }

        public void setNacionalidad(string nacionalidad)
        {
            _nacionalidad = nacionalidad;
        }

        public DateTime getFechaNacimiento()
        {
            return _fechaNacimiento;
        }

        public void setFechaNacimiento(DateTime fechaNacimiento)
        {
            _fechaNacimiento = fechaNacimiento;
        }

        public string getTelefono()
        {
            return _telefono;
        }

        public void setTelefono(string telefono)
        {
            _telefono = telefono;
        }

        public int getIdEspecialidad()
        {
            return _idEspecialidad;
        }

        public void setIdEspecialidad(int idEspecialidad)
        {
            _idEspecialidad = idEspecialidad;
        }

        public bool getEstado()
        {
            return _estado;
        }

        public void setEstado(bool estado)
        {
            _estado = estado;
        }
    }
}
