using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
    public partial class LOGIN : System.Web.UI.Page
    {
        NegocioUsuario negocio = new NegocioUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dtUsuarios = new DataTable();

            dtUsuarios = negocio.getTabla();

            bool coinciden = false;

            foreach (DataRow row in dtUsuarios.Rows)
            {
                string usuario = row["Usuario"].ToString().Trim();
                string contrasena = row["Contraseña"].ToString().Trim();

                string inputUsuario = txtUsuario.Text.Trim();
                string inputContrasena = txtContrasenia.Text.Trim();

                if (string.Equals(usuario, txtUsuario.Text.Trim(), StringComparison.Ordinal)&& string.Equals(contrasena, txtContrasenia.Text.Trim(), StringComparison.Ordinal)) 
                {
                    coinciden = true;
                    Session["IdUsuario"] = row["IdUsuario"];
                    Session["User"] = row["Usuario"];
                    Session["EsAdmin"] = Convert.ToBoolean(row["EsAdmin"]);
                    Session["LegajoMedico"] = row["legajo_medico"] == DBNull.Value ? null : row["legajo_medico"];//Pregunta si el legajo es null, si es null se mantiene null, si no, le da el valor del legajo

                    if ((bool)Session["EsAdmin"])
                        Response.Redirect("MenuAdmin.aspx");
                    else
                        Response.Redirect("MenuMedico.aspx");
                    break;
                }
            }

            if(!coinciden)
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                txtContrasenia.Text = null;
                txtUsuario.Text = null;
            }

        }
    }
}