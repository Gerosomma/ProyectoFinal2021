<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpresa.master" AutoEventWireup="true" CodeFile="ListadoSolicitudesEmpresa.aspx.cs" Inherits="ListadoSolicitudesEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <h2>Solicitudes Empresa</h2>
                </td>
            </tr>
            <tr>
                <td rowspan="4" style="vertical-align: top; text-align: left">
                    <asp:GridView ID="gvSolicitudes" runat="server" EmptyDataText="No existen solicitudes para la empresa logueada">
                    </asp:GridView>
                </td>
                <td>
                    <asp:Button ID="btnUltimoMes" runat="server" OnClick="btnUltimoMes_Click" Text="Ultimo Mes" Width="164px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnResumenMensual" runat="server" OnClick="btnResumenMensual_Click" Text="Resumen Mensual" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:Button ID="btnFecha" runat="server" OnClick="btnFecha_Click" Text="Fecha" Width="164px" />
                </td>
                <td>
                    <asp:Calendar ID="calFecha" runat="server" Caption="Fecha" CaptionAlign="Left"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar Filtros" Width="163px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

