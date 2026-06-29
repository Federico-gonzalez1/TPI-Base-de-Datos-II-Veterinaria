using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Reporte1 : System.Web.UI.Page
    {
        NegocioPacientes negocioPacientes = new NegocioPacientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        private int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (hoy.Month < fechaNacimiento.Month || (hoy.Month == fechaNacimiento.Month && hoy.Day < fechaNacimiento.Day))
            {
                edad--;
            }
            return edad;

        }

        protected void btnGenerarxEdad_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_Edad.Text, out int edadMinima) || edadMinima < 0)
            {
                lblMensaje.Text = "Edad no valida";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;

                return;
            }

            // Obtenemos todos los pacientes activos
            DataTable tablaPacientes = negocioPacientes.getTabla(); 

            tablaPacientes.Columns.Add("EDAD", typeof(int)); 

            int totalPacientes = tablaPacientes.Rows.Count;
            int mayoresDe = 0;
            DateTime hoy = DateTime.Today;

            foreach (DataRow row in tablaPacientes.Rows)
            {
                DateTime fechaNac = Convert.ToDateTime(row["FechaNacimiento"]);
                int edad = CalcularEdad(fechaNac);

                row["EDAD"] = edad; // agregamos la columna Edad

                if (edad >= edadMinima)
                {
                    mayoresDe++;
                }
            }

            DataView dv = new DataView(tablaPacientes);
            dv.RowFilter = $"Edad >= {edadMinima}";
            DataTable pacientesMayores = dv.ToTable();

            gvPacientes.DataSource = pacientesMayores;
            gvPacientes.DataBind();

            // Calculamos y mostramos porcentaje
            if (totalPacientes > 0)
            {
                float porcentaje = (float)mayoresDe/ totalPacientes * 100;
                lblPorcentaje.Text = "Total de pacientes: " + totalPacientes + ". Pacientes con " + edadMinima + " años o más: " + mayoresDe + ". Porcentaje: " + porcentaje.ToString("0.00") + "%";
                lblPorcentaje.ForeColor = System.Drawing.Color.Blue;
                
            }
            else
            {
                lblPorcentaje.Text = "No hay pacientes registrados.";
                lblPorcentaje.Visible = true;
            }

            lblMensaje.Visible = false;
        }

        protected void btnGenerarXFecha_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(tb_fecha.Text, out DateTime fechaMax))
            {
                lblMensaje1.Text = "Fecha ingresada no valida";
                lblMensaje1.ForeColor = System.Drawing.Color.Red;
                lblMensaje1.Visible = true;
                return;
            }

            // Traemos todos los pacientes activos
            DataTable tablaPacientes = negocioPacientes.getTabla();



            int totalPacientes = tablaPacientes.Rows.Count;
            int nacidosAntesContador = 0;
            DateTime hoy = DateTime.Today;

            foreach (DataRow row in tablaPacientes.Rows)
            {
                DateTime fechaNac = Convert.ToDateTime(row["FechaNacimiento"]);

                if (fechaNac < fechaMax.Date) // nacidos antes de la fecha (no incluye el día)
                {
                    nacidosAntesContador++;
                }
            }

            // Filtramos los nacidos antes de la fecha
            DataView dv = new DataView(tablaPacientes);
            dv.RowFilter = $"FechaNacimiento < #{fechaMax:MM/dd/yyyy}#"; // formato SQL Server
            DataTable pacientesFiltrados = dv.ToTable();

            gvPacientes1.DataSource = pacientesFiltrados;
            gvPacientes1.DataBind();

            // Porcentaje
            if (totalPacientes > 0)
            {
                float porcentaje = (float)nacidosAntesContador / totalPacientes * 100;
                lblPorcentaje.ForeColor = System.Drawing.Color.Blue;
                lblPorcentaje1.Text = "Total de pacientes: " + totalPacientes + ". Pacientes nacidos antes de " + fechaMax.ToString("dd/MM/yyyy") + ": " + nacidosAntesContador + ". Porcentaje: " + porcentaje.ToString("0.00") + "%";
                lblPorcentaje1.Visible = true;
            }
            else
            {
                lblPorcentaje1.Text = "No hay pacientes registrados.";
                lblPorcentaje1.Visible = true;
            }

            lblMensaje1.Visible = false;
        }
    }
    
}