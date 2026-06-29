<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoTurno.aspx.cs" Inherits="Vistas.NuevoTurno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            font-size: xx-large;
            width: 200px;
        }
        .auto-style3 {
            width: 189px;
        }
        .auto-style4 {
            width: 859px;
        }
        .auto-style5 {
            width: 28px;
        }
        .auto-style6 {
            width: 153px;
        }
        .auto-style7 {
            width: 200px;
        }
        .auto-style8 {
            width: 906px;
        }
        .auto-style9 {
            width: 204px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuAdmin.aspx">&lt;&lt;Volver al menu principal</asp:HyperLink>
                </td>
                <td class="auto-style4" colspan="2">&nbsp;</td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2"><strong>Nuevo Turno</strong></td>
                <td class="auto-style4" colspan="2">&nbsp;</td>
                <td class="auto-style5" colspan="2">Usuario:<asp:Label ID="lbl_sessionUser" runat="server"></asp:Label>
&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style4" colspan="2">&nbsp;</td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Especialidad: </td>
                <td class="auto-style4" colspan="2">
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" DataSourceID="SqlDataSource1" DataTextField="Descripcion" DataValueField="Id_Especialidad" AutoPostBack="true" OnDataBound="ddlEspecialidad_DataBound" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" ValidationGroup="vg1">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-CHHDRK8\SQLEXPRESS06;Initial Catalog=Clinica;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Id_Especialidad], [Descripcion] FROM [Especialidades]"></asp:SqlDataSource>--%>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=localhost\sqlexpress;Initial Catalog=Clinica;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Id_Especialidad], [Descripcion] FROM [Especialidades]"></asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ControlToValidate="ddlEspecialidad" InitialValue="" ErrorMessage="Debe seleccionar una especialidad" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Medico:</td>
                <td class="auto-style4" colspan="2">
                    <asp:DropDownList ID="ddlMedicos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged" ValidationGroup="vg1">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=DESKTOP-CHHDRK8\SQLEXPRESS06;Initial Catalog=Clinica;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Legajo], [Nombre], [Apellido] FROM [Medicos] WHERE ([Estado] = @Estado)">--%>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=localhost\sqlexpress;Initial Catalog=Clinica;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Legajo], [Nombre], [Apellido] FROM [Medicos] WHERE ([Estado] = @Estado)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="True" Name="Estado" Type="Boolean" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ControlToValidate="ddlMedicos" InitialValue="" ErrorMessage="Debe seleccionar un medico" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Fecha:</td>
                <td class="auto-style4" colspan="2">
                    <asp:Calendar ID="cal_fecha" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnDayRender="cal_fecha_DayRender" TodaysDate="<%# DateTime.Today %>" VisibleDate="<%# DateTime.Today %>" OnSelectionChanged="cal_fecha_SelectionChanged">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Horario:</td>
                <td class="auto-style4" colspan="2">
                    <asp:DropDownList ID="ddlHorarios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHorarios_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Seleccione un Paciente:</td>
                <td class="auto-style4" colspan="2">&nbsp;</td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style4" colspan="2">&nbsp;</td>
                <td class="auto-style5" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Buscar Paciente por DNI:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="Button1_Click" ValidationGroup="vg1" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:GridView ID="gvPacientes" runat="server" AutoPostBack="true" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Height="94px" PageSize="6" Width="770px" AutoGenerateSelectButton="True" OnPageIndexChanging="gvPacientes_PageIndexChanging" OnSelectedIndexChanged="gvPacientes_SelectedIndexChanged" OnSelectedIndexChanging="gvPacientes_SelectedIndexChanging">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:TemplateField HeaderText="DNI">
                                <ItemTemplate>
                                    <asp:Label ID="dniPaciente" runat="server" Text='<%# Bind("Dni") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NOMBRE COMPLETO">
                                <ItemTemplate>
                                    <asp:Label ID="nombreCompleto" runat="server" Text='<%# Eval("Nombre") + " " + Eval("Apellido") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SEXO">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem>Masculino</asp:ListItem>
                                        <asp:ListItem>Femenino</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Sexo" runat="server" Text='<%# Bind("Sexo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NACIONALIDAD">
                                <ItemTemplate>
                                    <asp:Label ID="Pais" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FECHA DE NACIMIENTO">
                                <ItemTemplate>
                                    <asp:Label ID="fechaNacimiento" runat="server" Text='<%# Eval("FechaNacimiento", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TELEFONO">
                                <ItemTemplate>
                                    <asp:Label ID="telefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lbl_seleccion" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Turno" Width="182px" OnClick="btnConfirmar_Click" ValidationGroup="vg1" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
