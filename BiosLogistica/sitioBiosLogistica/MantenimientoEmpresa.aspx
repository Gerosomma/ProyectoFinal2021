<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="MantenimientoEmpresa.aspx.cs" Inherits="MantenimientoEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Label ID="lblLogueo" runat="server" Text="Nombre de usuario: "></asp:Label>
        <asp:TextBox ID="txtLogueo" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña: "></asp:Label>
        <asp:TextBox ID="txtContrasena" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre completo: "></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono: "></asp:Label>
        <asp:TextBox ID="txtTelefono" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="lblDireccion" runat="server" Text="Dirección: "></asp:Label>
        <asp:TextBox ID="txtDireccion" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico: "></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Enabled="False" OnClick="btnAgregar_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Enabled="False" OnClick="btnModificar_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Enabled="False" OnClick="btnEliminar_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Formulario" OnClick="btnLimpiar_Click" /></div>
</asp:Content>

