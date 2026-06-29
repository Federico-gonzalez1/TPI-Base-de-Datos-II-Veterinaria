using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Vistas
{
    public partial class GestionPacientes : System.Web.UI.Page
    {
        NegocioPacientes negocioPacientes = new NegocioPacientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string usersession = Session["User"] as string;
                if (usersession != null)
                {
                    lbl_sessionUser.Text = usersession;
                }
                // Establecer los atributos min y max para el control de fecha de nacimiento
                tbFechanacimiento.Attributes.Add("min", DateTime.Now.AddYears(-110).ToString("yyyy-MM-dd"));
                tbFechanacimiento.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

                gvPacientes.DataSource = negocioPacientes.getTabla();

                gvPacientes.DataBind();

            }
        }
        private void CargarGridView()
        {
            gvPacientes.DataSource = negocioPacientes.getTabla();
            gvPacientes.DataBind();
        }

        protected void gvPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string dni = ((Label)gvPacientes.Rows[e.RowIndex].FindControl("dniPaciente")).Text;

            Paciente paciente = new Paciente(dni);

            NegocioPacientes negPacientes= new NegocioPacientes();

            negPacientes.bajaPaciente(paciente.getDni());
            CargarGridView();
        }


        protected void gvPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPacientes.EditIndex = e.NewEditIndex;
            CargarGridView();
            GridViewRow row = gvPacientes.Rows[e.NewEditIndex];
            TextBox tbFecha = (TextBox)row.FindControl("tb_eit_fechaNacimiento");
            if (tbFecha != null && DateTime.TryParse(tbFecha.Text, out DateTime fecha))
            {
                tbFecha.Text = fecha.ToString("yyyy-MM-dd");
            }
        }

        protected void gvPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPacientes.EditIndex = -1;
            CargarGridView();
        }

        protected void gvPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string dniPaciente = ((Label)gvPacientes.Rows[e.RowIndex].FindControl("lbl_eit_dni")).Text.Trim() ;
            string nombreCompleto = ((TextBox)gvPacientes.Rows[e.RowIndex].FindControl("tb_eit_nombreCompleto")).Text.Trim();

            string nombre = "";
            string apellido = "";

            if (!string.IsNullOrWhiteSpace(nombreCompleto))
            {
                var partes = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length >= 2)
                {
                    
                    apellido = partes[partes.Length - 1];
                    nombre = string.Join(" ", partes, 0, partes.Length - 1);
                }
                else if (partes.Length == 1)
                {

                    apellido = partes[0];
                    nombre = "";
                }
            }


            var row = gvPacientes.Rows[e.RowIndex];
            var ddl = (DropDownList)row.FindControl("ddlSexo");
            var hf = (HiddenField)row.FindControl("hfSexo");

            if (ddl != null && hf != null && !string.IsNullOrEmpty(hf.Value))
            {
                string sexo = hf.Value.Trim();  // quito los espacios del nchar(10)
                if (sexo == "Masculino" || sexo == "Femenino")
                    ddl.SelectedValue = sexo;
            }
            string sexoPaciente = ((Label)gvPacientes.Rows[e.RowIndex].FindControl("Sexo")).Text;
            string nacionalidadPaciente = ((TextBox)gvPacientes.Rows[e.RowIndex].FindControl("tb_eit_nacionalidad")).Text;
            string nacimientoPaciente = ((TextBox)gvPacientes.Rows[e.RowIndex].FindControl("tb_eit_fechaNacimiento")).Text;
            string telefonoPaciente = ((TextBox)gvPacientes.Rows[e.RowIndex].FindControl("tb_eit_telefono")).Text;

            Paciente paciente = new Paciente(dniPaciente, nombre, apellido, sexoPaciente, nacionalidadPaciente, Convert.ToDateTime(nacimientoPaciente), telefonoPaciente, true);

            negocioPacientes.actualizarPaciente(paciente);
            gvPacientes.EditIndex = -1;
            CargarGridView();
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            Paciente paciente = new Paciente(tbDni.Text.Trim(), tbNombre.Text.Trim(), tbApellido.Text.Trim(), ddlSexo.SelectedValue, tbNacionalidad.Text.Trim(), Convert.ToDateTime(tbFechanacimiento.Text), tbTelefono.Text.Trim(), true);

            //altaPaciente verifica si el paciente existe pero dado de baja, si es asi lo reactiva con los nuevos datos
            //tambien verifica si el dni ya existe en el sistema y en ese caso no lo da de alta
            if (negocioPacientes.altaPaciente(paciente)== true)
            {
                CargarGridView();
                tbDni.Text = "";
                tbNombre.Text = "";
                tbApellido.Text = "";
                ddlSexo.SelectedIndex = 0;
                tbNacionalidad.Text = "";
                tbFechanacimiento.Text = "";
                tbTelefono.Text = "";
                lblError.Text = "Paciente dado de alta correctamente.";
                CargarGridView();
            }
            else
            {
                //Mensaje de error
                lblError.Text="Error al dar de alta el paciente. Verifique que el DNI no exista en el sistema.";
            }

        }

        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarGridView();
            gvPacientes.PageIndex = e.NewPageIndex;
            gvPacientes.DataBind();
        }
    }
}