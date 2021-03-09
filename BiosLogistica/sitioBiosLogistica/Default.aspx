<%@ Page Title="" Language="C#" MasterPageFile="~/MPPublica.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 81px;
        }
        .auto-style2 {
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="text-align: center; position: relative; z-index: auto; width: 100%">
            <tr>
                <td style="font-size: xx-large;" colspan="5"><asp:Label ID="lblUsuario" runat="server" Text="¡BIENVENIDO! "></asp:Label></td>
                <td style="font-size: xx-large;" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: top" rowspan="3" class="auto-style1">
                    &nbsp;</td>
                <td style="vertical-align: top" colspan="4">
                    En este sitio nos encargamos de la logística de tus paquetes que necesites enviar,
                    <br />
                    a continuacion te mostramos en tiempo real los paquetes en curso actualmente.<br />
                </td>
                <td style="vertical-align: bottom">
                    <asp:Button ID="btnLogin" runat="server" Text="Acceder" OnClick="btnLogin_Click" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: bottom" class="auto-style2">
                    <asp:Calendar ID="calFechaEntrega" runat="server"></asp:Calendar>
                </td>
                <td style="vertical-align: bottom; text-align: left;">
                    <asp:Button ID="btnFiltrar" runat="server" OnClick="btnFiltrar_Click" Text="Filtrar fecha" />
                    <br />
                    <asp:Button ID="btnQuitar" runat="server" OnClick="btnQuitar_Click" Text="Quitar filtro fecha" />
                </td>
                <td style="vertical-align: top; text-align: left;" colspan="3" rowspan="2">
                    <asp:Xml ID="Xml1" runat="server" TransformSource="~/XSLT/detalleSolicitud.xslt"></asp:Xml>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left;" colspan="2">
                    <asp:GridView ID="gvSolicitudes" 
                        runat="server" 
                        AutoGenerateSelectButton="True" 
                        PageSize="100" 
                        HorizontalAlign="Left" 
                        OnSelectedIndexChanged="gvSolicitudes_SelectedIndexChanged" EmptyDataText="No existen solicitudes en camino"></asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="2">
                    &nbsp;</td>
                <td colspan="4" rowspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

