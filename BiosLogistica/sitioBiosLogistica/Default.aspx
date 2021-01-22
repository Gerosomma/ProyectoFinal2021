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
            <tr>
                <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateSelectButton="True" PageSize="100"></asp:GridView>
                <td>&nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

