<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="general">
        <h2>Login</h2>
        <table class="tabLogin">
            <tr class="tabTr">
                <th>Documento: </th>
                <th><asp:TextBox ID="txtDocumento" runat="server"></asp:TextBox></th>
            </tr>
            <tr class="tabTr">
                <th>Contraseña: </th>
                <th><asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox></th>
            </tr>
            <tr class="tabTr">
                <th></th>
                <th><asp:Button ID="btnLog" runat="server" Text="Login" /></th>
            </tr>
        </table>
        <asp:Label id="lblError" runat="server"></asp:Label>
    </div>
</asp:Content>

