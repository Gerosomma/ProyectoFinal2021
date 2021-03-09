<%@ Page Title="" Language="C#" MasterPageFile="~/MPPublica.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>Login</h2>
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1"></td>
                <td style="vertical-align: top; text-align: center; padding-left: 40%;">
                    <table class="tabLogin" style="text-align: center; vertical-align: middle;">
                        <tr class="tabTr">
                            <th>Usuario: </th>
                            <th><asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox></th>
                        </tr>
                        <tr class="tabTr">
                            <th>Contraseña: </th>
                            <th><asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" OnTextChanged="txtContrasena_TextChanged"></asp:TextBox></th>
                        </tr>
                        <tr class="tabTr">
                            <th></th>
                            <th><asp:Button ID="btnLog" runat="server" Text="Login" OnClick="btnLog_Click" /></th>
                        </tr>
                    </table>

                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label id="lblError" runat="server"></asp:Label></td>
            </tr>
        </table>


        
        
    </div>
</asp:Content>

