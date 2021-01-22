<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="general">
        <table style="width: 70%;">
            <tr>
                <td style="font-size: xx-large;"><asp:Label ID="lblUsuario" runat="server" Text="¡BIENVENIDO! "></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

