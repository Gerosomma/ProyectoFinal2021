<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="BienvenidaEmpleado.aspx.cs" Inherits="BienvenidaEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Bienvenido <asp:Label ID="lblUsuario" runat="server"></asp:Label>!</h1>
    <div>

        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>Has iniciado la pagina de mantenimeinto de BiosLogistica.</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: left">Acciones detalladas:<br />
                    <ul>
                        <li>Mantenimiento de Empresas</li>
                        <li>Mantenimiento de Empleados</li>
                        <li>Alta de paquetes</li>
                        <li>Listado de paquetes</li>
                        <li>Generar solicitudes de entregas</li>
                        <li>Avance en el estado de una solicitud</li>
                    </ul>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: center">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
</asp:Content>

