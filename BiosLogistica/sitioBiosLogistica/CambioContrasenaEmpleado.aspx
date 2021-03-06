<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="CambioContrasenaEmpleado.aspx.cs" Inherits="CambioContrasenaEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 50%;">
            <tr>
                <td style="text-align: left">&nbsp;</td>
                <td style="text-align: left">
                    <h3>Cambio contraseña usuario</h3>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">Contraseña actual:</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCnnActual" runat="server" Width="203px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">Nueva contraseña:</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCnnNueva" runat="server" Width="204px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">Repetir contraseña:</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCnnNuevaRep" runat="server" Width="204px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnCambiar" runat="server" Text="Enviar" OnClick="btnCambiar_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

