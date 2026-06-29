using Entidades;
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
    public partial class MenuMedico : System.Web.UI.Page
    {
        NegocioTurnos negocioTurnos = new NegocioTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarNombreMedico();
                CargarGridView();
            }

        }

        private void CargarGridView()
        {
            string user = Session["User"] as string;
            if(user == null)
            {
                gvTurnos.DataSource = negocioTurnos.getTablaTurnos(); 
                gvTurnos.DataBind();
                return;
            }

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                gvTurnos.DataSource = negocioTurnos.getTablaTurnos(); 
                gvTurnos.DataBind();
                return;
            }


            int legajo = Convert.ToInt32(Session["LegajoMedico"]);
            //gvTurnos.DataSource = negocioTurnos.getTablaTurnos();
            gvTurnos.DataSource = negocioTurnos.getTurnosXLegajoMedico(legajo);
            gvTurnos.DataBind();
        }

        protected void gvTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idTurno = Convert.ToInt32(gvTurnos.DataKeys[e.RowIndex].Value);

            DropDownList ddlEstado = (DropDownList)gvTurnos.Rows[e.RowIndex].FindControl("ddlEstado");
            TextBox tbObservaciones = (TextBox)gvTurnos.Rows[e.RowIndex].FindControl("tbObservaciones");

            int estadoSeleccionado = Convert.ToInt32(ddlEstado.SelectedValue);
            string observaciones = tbObservaciones.Text.Trim();

            Turno turnoActualizado = new Turno(idTurno, estadoSeleccionado, observaciones);

            if(negocioTurnos.actualizarTurno(turnoActualizado))
            {
                gvTurnos.EditIndex = -1;
                CargarGridView();
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el turno.";
            }
        }

        protected void gvTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTurnos.EditIndex = -1;
            CargarGridView();
        }

        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        private void MostrarObservaciones(DropDownList ddl, TextBox tb)
        {
            if (ddl.SelectedValue == "2")//Si se elige "PRESENTE"
            {
                tb.Visible = true;
            }
            else
            {
                tb.Visible = false;
            }
        }

        protected void gvTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTurnos.EditIndex = e.NewEditIndex;
            CargarGridView();
            GridViewRow row = gvTurnos.Rows[e.NewEditIndex];
            DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");
            TextBox tbObservaciones = (TextBox)row.FindControl("tbObservaciones");

            if (ddlEstado != null && tbObservaciones != null)
            {
                
                MostrarObservaciones(ddlEstado, tbObservaciones);
            }
        }

        protected void ddlEstado_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            TextBox tbObservaciones = (TextBox)row.FindControl("tbObservaciones");

            MostrarObservaciones(ddl, tbObservaciones);
        }

        protected void tb_buscarPaciente_TextChanged(object sender, EventArgs e)
        {
            NegocioTurnos negocioTurnos= new NegocioTurnos();
            string busqueda = tb_buscarPaciente.Text.Trim();

            if (!string.IsNullOrEmpty(busqueda))
            {
                DataTable pacientes = negocioTurnos.buscarTurnoXPaciente(busqueda);


                gvTurnos.DataSource = pacientes;
                gvTurnos.DataBind();
            }
            else
            {
                CargarGridView();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            FiltrarTurnos();
            tb_buscarPaciente.Text = string.Empty;
            tb_turnosDesde.Text = string.Empty;
            tb_turnosHasta.Text = string.Empty;
            ddlFiltroEstado.SelectedIndex = 0;
        }

        private void FiltrarTurnos()
        {

            lblMensaje.Visible = false;

            DateTime? desde = null;
            DateTime? hasta = null;


            bool tieneDesde = DateTime.TryParse(tb_turnosDesde.Text, out DateTime fechaDesde);
            bool tieneHasta = DateTime.TryParse(tb_turnosHasta.Text, out DateTime fechaHasta);

            if (tieneDesde)
                desde = fechaDesde;

            if (tieneHasta)
                hasta = fechaHasta;

            if ((tieneDesde && !tieneHasta) || (!tieneDesde && tieneHasta))
            {
                lblMensaje.Text = "Error: debe completar ambas fechas (Desde y Hasta) o dejar ambas vacías.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;

                
                CargarGridView();
                return;
            }

            if (desde.HasValue && hasta.HasValue)
            {
                if (desde.Value > hasta.Value)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Error: la fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.";
                    lblMensaje.Visible = true;
                    gvTurnos.DataSource = null;
                    gvTurnos.DataBind();
                    return;
                }
            }

            
            string estadoId = ddlFiltroEstado.SelectedValue;

           
            DataTable dt = negocioTurnos.buscarTurnosFiltrados(desde, hasta, estadoId);

            gvTurnos.DataSource = dt;
            gvTurnos.DataBind();
        }

        private void MostrarNombreMedico()
        {
            
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                apellidoMedico.Text = "(admin)";
                return;
            }

            int legajo = Convert.ToInt32(Session["LegajoMedico"]);

            
            NegocioMedicos negocioMedicos = new NegocioMedicos();
            Medico userMedico = negocioMedicos.getMedicoXLegajo(legajo);

            if (userMedico!=null)
            {
                apellidoMedico.Text = userMedico.getApellido();
            }

        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlFiltroEstado.SelectedIndex = 0;
            tb_buscarPaciente.Text = string.Empty;
            tb_turnosDesde.Text = string.Empty;
            tb_turnosHasta.Text = string.Empty;
            CargarGridView();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}