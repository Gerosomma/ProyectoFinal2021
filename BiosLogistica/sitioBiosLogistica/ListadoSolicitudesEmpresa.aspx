<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpresa.master" AutoEventWireup="true" CodeFile="ListadoSolicitudesEmpresa.aspx.cs" Inherits="ListadoSolicitudesEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <h2>Solicitudes Empresa</h2>
                </td>
            </tr>
            <tr>
                <td rowspan="4">
                    <asp:GridView ID="gvSolicitudes" runat="server">
                    </asp:GridView>
                </td>
                <td>
                    <asp:Button ID="btnUltimoMes" runat="server" OnClick="btnUltimoMes_Click" Text="Ultimo Mes" Width="164px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnResumenMensual" runat="server" OnClick="btnResumenMensual_Click" Text="Resumen Mensual" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Calendar ID="calFecha" runat="server" Caption="Fecha" CaptionAlign="Left"></asp:Calendar>
                    <asp:Button ID="btnFecha" runat="server" OnClick="btnFecha_Click" Text="Fecha" Width="164px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar Filtros" Width="163px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

