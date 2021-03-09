<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpresa.master" AutoEventWireup="true" CodeFile="BienvenidaEmpresa.aspx.cs" Inherits="BienvenidaEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Bienvenido <asp:Label ID="lblUsuario" runat="server"></asp:Label>!</h1>
    <div>

        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>Has iniciado la pagina de BiosLogistica.</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: left">Acciones detalladas:<br />
                    <ul>
                        <li>Ver solicitudes de la empresa</li>
                        <li>Reportes de solicitudes</li>
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

