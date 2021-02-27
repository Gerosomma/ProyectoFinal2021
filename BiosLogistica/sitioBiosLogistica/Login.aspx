﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MPPublica.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="general">
        <h2>Login</h2>
        <table class="tabLogin" style="position: relative; z-index: auto; text-align: center; vertical-align: middle; top: -14px; left: 563px;">
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
        <asp:Label id="lblError" runat="server"></asp:Label>
    </div>
</asp:Content>

