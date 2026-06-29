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
    public partial class Reporte2 : System.Web.UI.Page
    {
        NegocioTurnos negocioTurnos = new NegocioTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diaSeleccionado = ddlDiasSemana.SelectedValue;

            if (diaSeleccionado != "-1")
            {
            int diaSemana = Convert.ToInt32(diaSeleccionado);

            // Traemos directamente SOLO los ausentes del día elegido
            DataTable turnosAusentes = negocioTurnos.getTurnosAusentesXDia(diaSemana);

            gvTurnos.DataSource = turnosAusentes;
            gvTurnos.DataBind();

            int ausentesContador = turnosAusentes.Rows.Count;

            // Para el porcentaje necesitamos el total de turnos del día (con cualquier estado)
            int totalTurnosDia = negocioTurnos.getTotalTurnosXDia(diaSemana).Rows.Count;

            if (totalTurnosDia > 0)
            {
                float porcentaje = (float)ausentesContador/ totalTurnosDia * 100;
                lblPorcentaje.Text = "Ausentes el " + ddlDiasSemana.SelectedItem.Text + ": " + ausentesContador + " de " + totalTurnosDia + " turnos. Porcentaje: " + porcentaje.ToString("0.00") + "%";
                lblPorcentaje.ForeColor = System.Drawing.Color.Blue;
                lblPorcentaje.Visible = true;
            }
            else
            {
                lblPorcentaje.Text = "No hay turnos registrados ese dia.";
                lblPorcentaje.Visible = true;
            }

            lblMensaje.Visible = false;

            }

        }
    }
}