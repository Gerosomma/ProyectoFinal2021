<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="AltaSolicitud.aspx.cs" Inherits="AltaSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            width: 178px;
        }
        .auto-style4 {
            height: 26px;
            width: 178px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="3" class="auto-style1">
                    <h2>Generar solicitud de entrega</h2>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style3" style="text-align: left">Nombre destinatario</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNombre" runat="server" Width="93%"></asp:TextBox>
                </td>
                <td colspan="2" rowspan="3" style="vertical-align: top">
                    <asp:GridView ID="gvPaquetes2" runat="server" EmptyDataText="No hay paquetes disponibles" DataKeyNames="Codigo" EnablePersistedSelection="True">
                        <Columns>
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFF99" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="3px" />
                    </asp:GridView>
                    <asp:GridView ID="gvPaquetes"
                        DataKeyNames="Codigo"
                        OnRowCommand="CustomersGridView_RowCommand"
                        runat="server"
                        EmptyDataText="No hay paquetes disponibles">
                        <Columns>
                            <asp:ButtonField ButtonType="Button"
                                CommandName="Select"
                                HeaderText="Seleccion"
                                Text="Select" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style4" style="text-align: left">Dirección destinatario</td>
                <td class="auto-style2" style="text-align: left">
                    <asp:TextBox ID="txtDireccion" runat="server" Width="93%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="text-align: left; vertical-align: top;">Fecha a entregar</td>
                <td>
                    <asp:Calendar ID="calFEntrega" runat="server"></asp:Calendar>
                </td>
                </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="2" style="text-align: left">
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" OnClick="btnGenerar_Click" Width="178px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

