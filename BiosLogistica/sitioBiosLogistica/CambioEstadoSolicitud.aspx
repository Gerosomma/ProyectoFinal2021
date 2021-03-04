<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="CambioEstadoSolicitud.aspx.cs" Inherits="CambioEstadoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height: auto">
        <table style="width: 100%;">
            <tr>
                <td>
                    <h2>Cambio estado solicitud</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSolicitudes_RowCommand" EmptyDataText="No existen solicitudes pendientes " HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="Numero" HeaderText="Numero" />
                            <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha Entrega" />
                            <asp:BoundField DataField="NombreDestinatario" HeaderText="Nombre destinatario" />
                            <asp:BoundField DataField="DireccionDestinatario" HeaderText="Dirección destinatario" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Empleado.NombreCompleto" HeaderText="Empleado" />
                            <asp:ButtonField ButtonType="Button" Text="Avance" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

