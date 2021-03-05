<%@ Page Title="" Language="C#" MasterPageFile="~/MPPublica.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="general">
        <table style="text-align: center; position: relative; z-index: auto; width: 100%">
            <tr>
                <td style="font-size: xx-large;" colspan="5"><asp:Label ID="lblUsuario" runat="server" Text="¡BIENVENIDO! "></asp:Label></td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" rowspan="2">
                    &nbsp;</td>
                <td style="vertical-align: top" colspan="2">
                    <asp:GridView ID="gvSolicitudes" 
                        runat="server" 
                        AutoGenerateSelectButton="True" 
                        PageSize="100" 
                        HorizontalAlign="Left" 
                        OnSelectedIndexChanged="gvSolicitudes_SelectedIndexChanged" EmptyDataText="No existen solicitudes en camino"></asp:GridView>
                </td>
                <td style="vertical-align: top">
                    &nbsp;</td>
                <td style="vertical-align: top; text-align: left" rowspan="2">
                    <asp:Xml ID="Xml1" runat="server" TransformSource="~/XSLT/detalleSolicitud.xslt"></asp:Xml>
                </td>
                <td rowspan="2">&nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left;">
                    <asp:Button ID="btnFiltrar" runat="server" OnClick="btnFiltrar_Click" Text="Filtrar" />
                    <asp:Button ID="btnQuitar" runat="server" OnClick="btnQuitar_Click" Text="Quitar" />
                    <asp:Calendar ID="calFechaEntrega" runat="server"></asp:Calendar>
                </td>
                <td style="vertical-align: top; text-align: left;">
                    &nbsp;</td>
                <td style="vertical-align: top; text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

