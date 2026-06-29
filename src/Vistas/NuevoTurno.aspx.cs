using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class NuevoTurno : System.Web.UI.Page
    {
        NegocioMedicos negocio = new NegocioMedicos();
        NegocioPacientes negocioPacientes = new NegocioPacientes();
        NegocioHorarioAtencion negocioHorario = new NegocioHorarioAtencion();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string usersession = Session["User"] as string;
                if (usersession != null)
                {
                    lbl_sessionUser.Text = usersession;
                }
                //cargarMedicos();
                CargarGridView();
                ddlMedicos.Items.Clear();
                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar médico --", ""));
                ddlHorarios.Items.Insert(0, new ListItem("-- Seleccionar horario --", ""));
                ddlHorarios.Enabled = false;
                ActualizarCalendario();
                btnConfirmar.Enabled = false;
            }

        }

        private void cargarMedicos()
        {
            if(!string.IsNullOrEmpty(ddlEspecialidad.SelectedValue) && int.TryParse(ddlEspecialidad.SelectedValue, out int especialidadId))
            {

                NegocioMedicos negocioMedicos = new NegocioMedicos();

                DataTable dt = negocioMedicos.getMedicosEspecialidad(especialidadId);

                dt.Columns.Add("NombreCompleto", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string nombreCompleto = $"{row["Apellido"]}, {row["Nombre"]}".Trim();

                    row["NombreCompleto"] = nombreCompleto;
                }

                ddlMedicos.DataSource = dt;
                ddlMedicos.DataTextField = "NombreCompleto";
                ddlMedicos.DataValueField = "Legajo";   
                ddlMedicos.DataBind();

                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
            }


        }

        private void CargarGridView()
        {
            gvPacientes.DataSource = negocioPacientes.getTabla();
            gvPacientes.DataBind();
        }

        protected void ddlEspecialidad_DataBound(object sender, EventArgs e)
        {
            // Solo agrega la opción si  no esta
            if (ddlEspecialidad.Items.FindByText("-- Seleccionar --") == null)
            {
                ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarGridView();
        }

        protected void cal_fecha_DayRender(object sender, DayRenderEventArgs e)
        {


            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
            }

            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            }
            
            if (string.IsNullOrEmpty(ddlMedicos.SelectedValue) || ddlMedicos.SelectedValue=="")
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                e.Cell.ToolTip = "Seleccione un médico para ver los días disponibles.";
                return;
            }
            
                int legajoMedico = int.Parse(ddlMedicos.SelectedValue);

                int diaElegido = (int)e.Day.Date.DayOfWeek + 1;
                NegocioHorarioAtencion negocio = new NegocioHorarioAtencion();
                
                bool atiende = negocio.MedicoAtiendeTalDia(legajoMedico, diaElegido);

            if (atiende)
            {
                e.Cell.BackColor = System.Drawing.Color.LightGreen;
                e.Cell.ToolTip = "El médico atiende este día";
            }
            else
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray ;
                e.Cell.ToolTip = "El médico no atiende este día";
            }

        }

        private void CargarHorariosDisponibles()
        {
            ddlHorarios.Items.Clear();
            //lblNoHayHorarios.Visible = false;
            ddlHorarios.Items.Add(new ListItem("-- Seleccionar horario --", ""));

            if (ddlMedicos.SelectedValue == "" || cal_fecha.SelectedDate == DateTime.MinValue)
            {
                ddlHorarios.Enabled = false;
                return;
            }

            int idMedico = Convert.ToInt32(ddlMedicos.SelectedValue);
            int diaSemana = (int)cal_fecha.SelectedDate.DayOfWeek + 1;


            DataTable horarios = negocioHorario.getHorariosMedico(idMedico, diaSemana);
            DataTable turnosReservados = new NegocioTurnos().getTurnosMedicoFecha(Convert.ToInt32(ddlMedicos.SelectedValue), cal_fecha.SelectedDate);

            if (horarios.Rows.Count == 0)
            {
                ddlHorarios.Items.Clear();
                ddlHorarios.Items.Add(new ListItem("No hay horarios disponibles", ""));
                ddlHorarios.Enabled = false;
                //lblNoHayHorarios.Visible = true;
                return;
            }
            
                ddlHorarios.Enabled = true;
                foreach (DataRow row in horarios.Rows)
                {
                    //divido el rango horario en bloques de una hora

                    //consigo hora inicio y fin y los declaro en variable tipo TimeSpan
                    TimeSpan inicio = (TimeSpan)row["HoraInicio"];
                    TimeSpan fin = (TimeSpan)row["HoraFin"];

                    //mientras que la hora de inicio + 1 hora sea menor o igual a la hora fin
                    //creo un turno de una hora
                    while (inicio.Add(TimeSpan.FromHours(1)) <= fin)
                    {
                        TimeSpan turnoFin = inicio.Add(TimeSpan.FromHours(1));

                        bool reservado = false;
                        //verifico si el turno ya esta reservado

                        foreach(DataRow r in turnosReservados.Rows)
                        {
                            TimeSpan hora = TimeSpan.Parse(r["Hora"].ToString());
                            if (hora == inicio)
                            {
                                reservado = true;
                                break;
                            }
                        }

                    if (!reservado)
                    {
                        string texto = $"{inicio:hh\\:mm} - {turnoFin:hh\\:mm}";
                        string value = inicio.ToString(@"hh\:mm");

                        ddlHorarios.Items.Add(new ListItem(texto, value));
                    }

                        inicio = turnoFin;
                    }
                }

            if (ddlHorarios.Items.Count == 1) // Solo el "-- Seleccionar --"
            {
                ddlHorarios.Items.Clear();
                ddlHorarios.Items.Add(new ListItem("Todos los turnos reservados", ""));
                ddlHorarios.Enabled = false;
            }


        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarMedicos();
            ActualizarCalendario();
        }

        protected void cal_fecha_SelectionChanged(object sender, EventArgs e)
        {
            CargarHorariosDisponibles();

        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargarHorariosDisponibles();
            ActualizarCalendario();


        }

        private void ActualizarCalendario()
        {
            
            if(cal_fecha.SelectedDate == DateTime.MinValue)
            {
                cal_fecha.VisibleDate = DateTime.Today;
            }

            cal_fecha.DataBind();
            cal_fecha.VisibleDate = cal_fecha.SelectedDate;
        }

        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarGridView();
            gvPacientes.PageIndex = e.NewPageIndex;
            gvPacientes.DataBind();
        }

        protected void gvPacientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string dni = (gvPacientes.Rows[e.NewSelectedIndex].FindControl("dniPaciente") as System.Web.UI.WebControls.Label).Text;
            string nombreCompleto = (gvPacientes.Rows[e.NewSelectedIndex].FindControl("nombreCompleto") as System.Web.UI.WebControls.Label).Text;
            lbl_seleccion.Text="Ha seleccionado a: "+ nombreCompleto + " (DNI: " + dni + ")";
        }

        protected void gvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarEstadoBotonConfirmar();
        }

        protected void ddlHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarEstadoBotonConfirmar();
        }

        private void cambiarEstadoBotonConfirmar()
        {
            bool pacienteSeleccionado = gvPacientes.SelectedIndex >= 0;
            bool horarioSeleccionado = !string.IsNullOrEmpty(ddlHorarios.SelectedValue);

            btnConfirmar.Enabled = pacienteSeleccionado && horarioSeleccionado;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            NegocioTurnos negocioTurnos = new NegocioTurnos();
            string dniPaciente = (gvPacientes.SelectedRow.FindControl("dniPaciente") as System.Web.UI.WebControls.Label).Text;
            TimeSpan horaElegida = TimeSpan.Parse(ddlHorarios.SelectedValue);
            Turno turno = new Turno(cal_fecha.SelectedDate,horaElegida,dniPaciente,Convert.ToInt32(ddlMedicos.SelectedValue), 1);
            if (negocioTurnos.agregarTurno(turno) == true)
            {
                CargarGridView();
                ddlEspecialidad.SelectedIndex = 0;
                ddlMedicos.Items.Clear();
                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar médico --", ""));
                ddlHorarios.Items.Clear();
                //lblError.Text = "Paciente dado de alta correctamente.";
                CargarGridView();
                gvPacientes.SelectedIndex = -1;
                lbl_seleccion.Text = "";
                btnConfirmar.Enabled = false;
                TextBox2.Text = "";
            }
            else
            {
                //Mensaje de error
                //lblError.Text = "Error al dar de alta el paciente. Verifique que el DNI no exista en el sistema.";
                btnConfirmar.Enabled = false;
                return;
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            NegocioPacientes negocioPacientes= new NegocioPacientes();
            string busqueda = TextBox2.Text.Trim();
           
            if (!string.IsNullOrEmpty(busqueda))
            {
                DataTable pacientes = negocioPacientes.buscarPacientes(busqueda);


                gvPacientes.DataSource = pacientes;
                gvPacientes.DataBind();
                
            }
            else
            {
                CargarGridView();
            }
        }
    }

    //LLENAR BASE DE DATOS





    //hacer interfaz de medico para turnos. Si ya paso la fecha, opcion para Presente o ausente. SI presente, opcion para observacion. TICK

    //agregar reportes. TICK

    //comentar y corregir codigo

    //diferenciar mayusculas en el login y password

    //mensaje bienvenido doctor con getApellido medico logueado.TICK

    //chequear 15 puntos. VER PUNTO 7, PUNTO 11, PUNTO 15

    //agregar algun filtro o busqueda en los gv

    //agregar localidad, direccion y provincia a medicos y pacientes.

    //agregar un poco de front o detalles esteticos

    //LLENAR BASE DE DATOS
}