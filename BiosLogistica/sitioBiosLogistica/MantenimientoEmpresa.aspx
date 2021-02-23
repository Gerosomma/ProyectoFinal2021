<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="MantenimientoEmpresa.aspx.cs" Inherits="MantenimientoEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 50%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblLogueo" runat="server" Text="Nombre de usuario: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtLogueo" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtContrasena" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="margin-left: 40px">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre completo: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="margin-left: 40px">
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtTelefono" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="margin-left: 40px">
                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtDireccion" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="margin-left: 40px">
                    <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtEmail" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="margin-left: 40px">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Enabled="False" OnClick="btnAgregar_Click" />
                </td>
                <td style="margin-left: 40px">
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" Enabled="False" OnClick="btnModificar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Enabled="False" OnClick="btnEliminar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Formulario" OnClick="btnLimpiar_Click" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="margin-left: 40px">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

