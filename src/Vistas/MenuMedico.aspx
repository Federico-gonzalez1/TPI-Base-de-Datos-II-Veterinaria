<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuMedico.aspx.cs" Inherits="Vistas.MenuMedico" %>

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
            width: 568px;
        }
        .auto-style3 {
            height: 23px;
        }
        .auto-style4 {
            width: 568px;
            height: 23px;
        }
        .auto-style5 {
            height: 192px;
        }
        .auto-style6 {
            width: 568px;
            height: 192px;
        }
        .auto-style7 {
            height: 27px;
        }
        .auto-style8 {
            width: 568px;
            height: 27px;
        }
        .auto-style13 {
            width: 434px;
            height: 23px;
        }
        .auto-style14 {
            width: 467px;
            height: 23px;
            text-align: center;
        }
        .auto-style17 {
            width: 365px;
            height: 23px;
        }
        .auto-style18 {
            width: 467px;
            height: 23px;
        }
        .auto-style20 {
            width: 434px;
            height: 23px;
            text-decoration: underline;
            font-size: large;
        }
        .auto-style21 {
            width: 360px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" colspan="7">BIENVENIDO DR/A&nbsp; <asp:Label ID="apellidoMedico" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" colspan="7">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style20">Mis Turnos<br />
                </td>
                <td class="auto-style14">Filtrar por fecha(dd/mm/aa)</td>
                <td class="auto-style4" colspan="3">&nbsp;</td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style13">Buscar Paciente: <asp:TextBox ID="tb_buscarPaciente" AutoPostBack="true" runat="server" Width="175px" OnTextChanged="tb_buscarPaciente_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style18">Turnos desde:
                    <asp:TextBox ID="tb_turnosDesde" runat="server" Width="117px" TextMode="Date"></asp:TextBox>
                </td>
                <td class="auto-style21">Hasta:<asp:TextBox ID="tb_turnosHasta" runat="server" Width="118px" TextMode="Date"></asp:TextBox>
                </td>
                <td class="auto-style17">Estado: <asp:DropDownList ID="ddlFiltroEstado" runat="server">
                        <asp:ListItem>TODOS</asp:ListItem>
                        <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
                        <asp:ListItem Value="2">PRESENTE</asp:ListItem>
                        <asp:ListItem Value="3">AUSENTE</asp:ListItem>
                        <asp:ListItem Value="4">CANCELADO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style17">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" Width="105px" />
                </td>
                <td class="auto-style4" colspan="2">
                    <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" OnClick="btnLimpiarFiltros_Click" />
                </td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" colspan="7">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            </table>
        <p>
        </p>
        <table class="auto-style1">
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style6">
                    <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" AutoPostBack="true" DataKeyNames="Id_Turno" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="750px" AutoGenerateEditButton="True" OnRowCancelingEdit="gvTurnos_RowCancelingEdit" OnRowEditing="gvTurnos_RowEditing" OnRowUpdating="gvTurnos_RowUpdating" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="fecha" runat="server" Text='<%# Eval("Fecha", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora">
                                <ItemTemplate>
                                    <asp:Label ID="horaTurno" runat="server" Text='<%# ((TimeSpan)Eval("Hora")).ToString(@"hh\:mm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paciente">
                                <ItemTemplate>
                                    <asp:Label ID="NombrePaciente" runat="server" Text='<%# Eval("NombreCompletoPaciente") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEstado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged1" SelectedValue='<%# Bind("Estado") %>'>
                                        <asp:ListItem Value="1">Pendiente</asp:ListItem>
                                        <asp:ListItem Value="2">Presente</asp:ListItem>
                                        <asp:ListItem Value="3">Ausente</asp:ListItem>
                                        <asp:ListItem Value="4">Cancelado</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_estado" runat="server" Text='<%# Eval("DescripcionEstado") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observaciones">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbObservaciones" runat="server" AutoPostBack="True" Height="43px" Width="317px" Text='<%# Bind("Observaciones") %>' TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblObservaciones" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
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
                <td class="auto-style5"></td>
                <td class="auto-style5"></td>
                <td class="auto-style5"></td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style7"></td>
                <td class="auto-style8">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style4"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesion" />
                </td>
                <td class="auto-style4"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
