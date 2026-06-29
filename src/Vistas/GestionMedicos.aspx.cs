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
    public partial class MenuMedicos : System.Web.UI.Page
    {
        NegocioMedicos negocio = new NegocioMedicos();
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
                tb_fechaNacimiento.Attributes.Add("min", DateTime.Now.AddYears(-110).ToString("yyyy-MM-dd"));
                tb_fechaNacimiento.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

                gvMedicos.DataSource = negocio.getTabla();
                gvMedicos.DataBind();

                InicializarGvHorarios();
            }

        }

        private void InicializarGvHorarios()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DiaSemana", typeof(string));
            dt.Columns.Add("HoraInicio", typeof(string));
            dt.Columns.Add("HoraFin", typeof(string));

            DataRow dummyRow = dt.NewRow();
            dummyRow["DiaSemana"] = "dummy";  // valor irrelevante
            dummyRow["HoraInicio"] = " ";
            dummyRow["HoraFin"] = " ";
            dt.Rows.Add(dummyRow);

            ViewState["Horarios"] = dt;

            gvHorarios.DataSource = dt;
            gvHorarios.DataBind();
            if (gvHorarios.Rows.Count > 0)
            {
                gvHorarios.Rows[0].Visible = false;  // la primera fila la agrego yo
            }
        }

        private void BindgvHorarios()
        {
            DataTable dt = (DataTable)ViewState["Horarios"];


            for(int i=dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dt.Rows[i];
                if (row["DiaSemana"].ToString() == "dummy")
                {
                    dt.Rows.RemoveAt(i);
                }
            }

            // RECREAR LA DUMMY SOLO SI NO QUEDA NINGUNA FILA REAL
            if (dt.Rows.Count == 0)
            {
                DataRow dummy = dt.NewRow();
                dummy["DiaSemana"] = "dummy";
                //dummy["HoraInicio"] = "";
                //dummy["HoraFin"] = "";
                dt.Rows.Add(dummy);
            }
            ViewState["Horarios"] = dt;
            gvHorarios.DataSource = dt;
            gvHorarios.DataBind();
        }

        protected void gvMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string legajo = ((Label)gvMedicos.Rows[e.RowIndex].FindControl("lblLegajo")).Text;

            Medico medico = new Medico(Convert.ToInt32(legajo));

            NegocioMedicos negMedicos = new NegocioMedicos();

            negMedicos.bajaMedico(medico.getLegajo());
            CargarGridView();
        }

        private void CargarGridView()
        {
            gvMedicos.DataSource = negocio.getTabla();
            gvMedicos.DataBind();
        }

        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int legajo = Convert.ToInt32(((Label)gvMedicos.Rows[e.RowIndex].FindControl("lbl_eit_legajo")).Text);
            string dniMedico = ((Label)gvMedicos.Rows[e.RowIndex].FindControl("lbl_eit_dni")).Text;
            string nombreCompleto = ((TextBox)gvMedicos.Rows[e.RowIndex].FindControl("tb_eit_nombreCompleto")).Text;

            string nombre = "";
            string apellido = "";

            if (!string.IsNullOrWhiteSpace(nombreCompleto))
            {
                //guarda las partes del nombre en un array separados por un espacio, y elimina los espacios vacoss
                var partes = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length >= 2)
                {
                    // todo lo que está antes del ultimo espacio = Nombre
                    // el ultimo input = Apellido
                    apellido = partes[partes.Length - 1];
                    //Asigno a la variable nombre todas las partes menos el apellido
                    nombre = string.Join(" ", partes, 0, partes.Length - 1);
                }
                else if (partes.Length == 1)
                {
                    
                    apellido = partes[0];
                    nombre = "";
                }
            }

            string sexoMedico = ((Label)gvMedicos.Rows[e.RowIndex].FindControl("Sexo")).Text;
            string nacionalidadMedico= ((TextBox)gvMedicos.Rows[e.RowIndex].FindControl("tb_eit_nacionalidad")).Text;
            string nacimientoMedico = ((TextBox)gvMedicos.Rows[e.RowIndex].FindControl("tb_eit_fechaNacimiento")).Text;
            string telefonoMedico = ((TextBox)gvMedicos.Rows[e.RowIndex].FindControl("tb_eit_telefono")).Text;
            //int idEspecialidad = Convert.ToInt32(((Label)gvMedicos.Rows[e.RowIndex].FindControl("lbl_eit_especialidad")).Text);
            var row1 = gvMedicos.Rows[e.RowIndex];

            // ... tu código anterior para nombre completo, sexo, etc.

            int idEspecialidadId = Convert.ToInt32(
                ((DropDownList)row1.FindControl("ddlEspecialidad")).SelectedValue
            );


            Medico medico = new Medico(legajo, dniMedico, nombre, apellido, sexoMedico, nacionalidadMedico, Convert.ToDateTime(nacimientoMedico), telefonoMedico, idEspecialidadId);

            NegocioMedicos negocioMedicos = new NegocioMedicos();
            negocioMedicos.actualizarMedico(medico);
            gvMedicos.EditIndex = -1;
            CargarGridView();
        }

        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;
            CargarGridView();
        }

        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMedicos.EditIndex = e.NewEditIndex;
            CargarGridView();
        }

        protected void btn_Alta_Click(object sender, EventArgs e)
        {
            //guardo el username ingresado en el textbox
            string username = tb_user.Text.Trim();

            //creo un objeto usuario con el username ingresado
            Usuario nuevoUsuario = new Usuario(username);

            NegocioUsuario negocioUsuario = new NegocioUsuario();

            //Si el usuario no existe, se crea el usuario y el medico. Si existe, se muestra un mensaje de error.
            if (negocioUsuario.existeUsuario(nuevoUsuario) == true)
            {
                Label1.Text = "El usuario ya existe.";
                return;

            }
            else
            {
                //si no coinciden las contraseñas
                if (tb_contra.Text.Trim() != tb_confirmarContra.Text.Trim())
                {
                    Label1.Text = "Las contraseñas no coinciden.";
                    return;
                }

                //guardo todos los datos del medico ingresados por el usuario
                string dni = tb_dni.Text.Trim();
                string nombre=tb_nombre.Text.Trim();
                string apellido= tb_apellido.Text.Trim();
                string sexo = ddlSexo.SelectedValue;
                string nacionalidad= tb_Nacionalidad.Text.Trim();
                DateTime fechaNacimiento= Convert.ToDateTime(tb_fechaNacimiento.Text);
                string telefono= tb_telefono.Text.Trim();
                //ddlEspecialidades.SelectedValue = null;
                int idEspecialidad= Convert.ToInt32(ddlEspecialidades.SelectedValue);
                bool estado = true;

                Label1.Text = idEspecialidad.ToString();


                DataTable dtHorarios = (DataTable)ViewState["Horarios"];

                int cantHorarios = dtHorarios.Rows.Count;
                if (cantHorarios > 1) cantHorarios--;

                if(cantHorarios == 0)
                {
                    Label1.Text = "Debe ingresar al menos un horario de atención.";
                    return;
                }

                //Creo el objeto medico con los datos ingresados para pasarselo por parametro el metodo del alta
                Medico medico = new Medico(dni, nombre, apellido, sexo, nacionalidad, fechaNacimiento, telefono, idEspecialidad, estado);

                

                NegocioMedicos negocio = new NegocioMedicos();

                //en este if si hubo un problema en la bdd para dar de alta al medico hago return.
                //en el else sigo con el alta del usuario en caso de que se pudo dar de alta el medico
                if (negocio.altaMedico(medico)==false)
                {
                    Label1.Text = "No se pudo dar de alta el medico. Verifique que no exista el DNI.";
                    return;
                }
                else if(dtHorarios.Rows.Count ==1 && dtHorarios.Rows[0]["DiaSemana"].ToString() == "dummy")
                {
                    Label1.Text = "Debe ingresar al menos un horario de atención.";
                    return;
                }
                else
                {
                    Label1.Text = "Ya se dio de alta el medico";

                    //obtengo el medico recien dado de alta para obtener su legajo
                    Medico medico1 = new Medico(negocio.getMedico(dni));


                    //Si se da de alta el medico, se da de alta el usuario
                    string contrasena = tb_contra.Text.Trim();
                    Usuario usuario = new Usuario(username, contrasena, false, medico1.getLegajo(), true);

                    if (negocioUsuario.altaUsuario(usuario) == false)
                    {
                        Label1.Text = "No se pudo dar de alta el usuario.";
                            return;
                    }
                    Label1.Text = "Medico dado de alta correctamente.";

                        //negocioUsuario.altaUsuario(usuario);
                    NegocioHorarioAtencion negocioHorario = new NegocioHorarioAtencion();
                    //DataTable dtHorarios = (DataTable)ViewState["Horarios"];
                    HorarioAtencion horarioAtencion = new HorarioAtencion();
                    Medico medico2 = new Medico(negocio.getMedico(dni));
                    foreach (DataRow row in dtHorarios.Rows)
                    {
                        int legajoMedico = medico2.getLegajo();
                        string diaSemana = row["DiaSemana"].ToString();
                        string horaInicio = row["HoraInicio"].ToString();
                        string horaFin = row["HoraFin"].ToString();

                        if (dtHorarios.Rows.Count == 1 && diaSemana == "dummy")
                        {
                            break;
                        }
                        if (!TimeSpan.TryParse(horaInicio, out TimeSpan inicio) || !TimeSpan.TryParse(horaFin, out TimeSpan fin) || fin <= inicio)
                        {
                            lblMensaje.Text = "Error de horario en el dia: " + diaSemana;
                            return;
                        }


                        HorarioAtencion ha = new HorarioAtencion(legajoMedico, ConvertirDiaANumero(diaSemana), inicio, fin, true);
                        negocioHorario.agregarHorarioAtencion(ha);
                    }
                }


            }
                
                
                CargarGridView();
            ClearForm();
        }

        private void ClearForm()
        {
            tb_user.Text = "";
            tb_dni.Text = "";
            tb_nombre.Text = "";
            tb_apellido.Text = "";
            ddlSexo.SelectedIndex = 0;
            tb_Nacionalidad.Text = "";
            tb_fechaNacimiento.Text = "";
            tb_telefono.Text = "";
            ddlEspecialidades.SelectedIndex = 0;
            InicializarGvHorarios();
        }

        private int ConvertirDiaANumero(string dia)
        {
            switch (dia)
            {
                case "Lunes": return 2;
                case "Martes": return 3;
                case "Miercoles": return 4;
                case "Jueves": return 5;
                case "Viernes": return 6;
                case "Sabado": return 7;
                case "Domingo": return 1;
                default: return 0; // error
            }
        }

        protected void gvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMedicos.PageIndex = e.NewPageIndex;
                        gvMedicos.DataBind();
            CargarGridView();
        }

        protected void gvHorarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                DropDownList ddlDia = (DropDownList)gvHorarios.FooterRow.FindControl("ddlDia");
                TextBox txtInicio = (TextBox)gvHorarios.FooterRow.FindControl("txtInicio");
                TextBox txtFin = (TextBox)gvHorarios.FooterRow.FindControl("txtFin");

                if (string.IsNullOrEmpty(txtInicio.Text) || string.IsNullOrEmpty(txtFin.Text))
                {
                    lblMensaje.Text = "Complete las horas";
                    return;
                }

                if (!TimeSpan.TryParse(txtInicio.Text, out TimeSpan inicio) || !TimeSpan.TryParse(txtFin.Text, out TimeSpan fin) || fin <= inicio)
                {
                    lblMensaje.Text = "Error de horario en el dia: " + ddlDia.SelectedItem;
                    return;
                }

                DataTable dt = (DataTable)ViewState["Horarios"];
                DataRow dr = dt.NewRow();
                dr["DiaSemana"] = ddlDia.SelectedItem.Text;
                dr["HoraInicio"] = txtInicio.Text;
                dr["HoraFin"] = txtFin.Text;
                dt.Rows.Add(dr);
                ViewState["Horarios"] = dt;

                BindgvHorarios();
                lblMensaje.Text = "";
            }
            else if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument); // indice de la fila a eliminar
                DataTable dt = (DataTable)ViewState["Horarios"];
                dt.Rows.RemoveAt(index);
                ViewState["Horarios"] = dt;
                BindgvHorarios();
            }
        }


    }
}