<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="AltaPaquetes.aspx.cs" Inherits="AltaPaquetes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Label ID="lblCodigo" runat="server" Text="Código: "></asp:Label>
        <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblTipo" runat="server" Text="Tipo: "></asp:Label>
        <asp:DropDownList ID="ddlTipo" runat="server">
            <asp:ListItem Selected="True" Value="comun">Común</asp:ListItem>
            <asp:ListItem Value="fragil">Frágil</asp:ListItem>
            <asp:ListItem>Bulto</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblPeso" runat="server" Text="Peso: "></asp:Label>
        <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: "></asp:Label>
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa: "></asp:Label>
        <asp:ListBox ID="lbxEmpresa" runat="server"></asp:ListBox>
        <br />
        <asp:Button ID="btnAlta" runat="server" Text="Agregar Paquete" OnClick="btnAlta_Click" />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

