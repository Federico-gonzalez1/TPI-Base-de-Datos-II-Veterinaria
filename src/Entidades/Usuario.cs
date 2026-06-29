using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        private int IdUsuario;
        private string NombreUsuario;
        private string Contrasenia;
        private bool EsAdmin;
        private int Legajo_medico;
        private bool Estado;


        public Usuario()
        { }

        public Usuario(string nombreusuario)
        { 
            NombreUsuario = nombreusuario;
        }
        public Usuario(string nombreusuario, string contrasenia, bool esadmin, int legajo, bool estado)
        {
            NombreUsuario = nombreusuario;
            Contrasenia = contrasenia;
            EsAdmin = esadmin;
            Legajo_medico = legajo;
            Estado = estado;
        }
        public int getIdUsuario()
        {
            return IdUsuario;
        }

        public String getNombreUsuario()
        {
            return NombreUsuario;
        }
        public void setNombreUsuario(String nombreUsuario)
        {
            NombreUsuario = nombreUsuario;
        }
        public String getContrasenia()
        {
            return Contrasenia;
        }
        public void setContrasenia(String contrasenia)
        {
            Contrasenia = contrasenia;
        }

        public bool getEsAdmin()
        {
            return EsAdmin;
        }

        public void setEsAdmin(bool esAdmin)
        {
            EsAdmin = esAdmin;
        }

        public int getLegajoMedico()
        {
            return Legajo_medico;
        }

        public void setLegajoMedico(int legajoMedico)
        {
            Legajo_medico = legajoMedico;
        }

        public bool getEstado()
        {
            return Estado;
        }

        public void setEstado(bool estado)
        {
            Estado = estado;
        }
    }
}
