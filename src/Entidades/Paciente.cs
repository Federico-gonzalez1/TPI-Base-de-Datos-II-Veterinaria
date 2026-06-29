using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente
    {
        string _dni;
        string _nombre;
        string _apellido;
        string _sexo;
        string _nacionalidad;
        DateTime _fechaNacimiento;
        string _telefono;
        bool _estado;

        public Paciente()
        { }

        public Paciente(string dni)
        {
            _dni = dni;
        }

        public Paciente(string dni, string nombre, string apellido, string sexo, string nacionalidad, DateTime fechaNacimiento, string telefono, bool estado)
        {
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _sexo = sexo;
            _nacionalidad = nacionalidad;
            _fechaNacimiento = fechaNacimiento;
            _telefono = telefono;
            _estado = estado;
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
