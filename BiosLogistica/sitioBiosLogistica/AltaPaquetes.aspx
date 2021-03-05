<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="AltaPaquetes.aspx.cs" Inherits="AltaPaquetes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            height: 35px;
        }
        .auto-style3 {
            height: 23px;
            width: 209px;
        }
        .auto-style4 {
            height: 35px;
            width: 209px;
        }
        .auto-style5 {
            width: 209px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <h2>Alta de Paquete</h2>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" style="text-align: left">

                    &nbsp;</td>
                <td class="auto-style1" style="text-align: left">

                    <asp:Label ID="lblCodigo" runat="server" Text="Código de Barras: "></asp:Label>
                </td>
                <td class="auto-style1" style="text-align: left">
                    <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td style="text-align: left" class="auto-style4">
                    &nbsp;</td>
                <td style="text-align: left" class="auto-style2">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de paquete: "></asp:Label>
                </td>
                <td style="text-align: left" class="auto-style2">
                    <asp:DropDownList ID="ddlTipo" runat="server">
                        <asp:ListItem Selected="True" Value="comun">Común</asp:ListItem>
                        <asp:ListItem Value="fragil">Frágil</asp:ListItem>
                        <asp:ListItem Value="bulto">Bulto</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td style="text-align: left" class="auto-style5">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Label ID="lblPeso" runat="server" Text="Peso gr.: "></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;" class="auto-style5">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: "></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;" class="auto-style5">
                    &nbsp;</td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa proveniente: "></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:ListBox ID="lbxEmpresa" runat="server"></asp:ListBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Button ID="Button1" runat="server" Text="Limpiar formulario" OnClick="Button1_Click" />
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btnAlta" runat="server" Text="Agregar Paquete" OnClick="btnAlta_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

