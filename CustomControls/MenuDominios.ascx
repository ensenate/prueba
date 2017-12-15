<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuDominios.ascx.cs" Inherits="CustomControls_MenuDominios" %>
<div id="menu" align="center">
          <asp:LinkButton ID="lnkAdmin" runat="server" 
            PostBackUrl="~/Administracion/Default.aspx">Administracion</asp:LinkButton>
      
             <asp:LinkButton ID="lnkCategoria" runat="server" onclick="lnkCategoria_Click" >Categoria</asp:LinkButton>


        <asp:LinkButton ID="lnkMarca" runat="server" onclick="lnkMarca_Click">Marcas</asp:LinkButton>


        <asp:LinkButton ID="lnkModelo" runat="server" onclick="lnkModelo_Click">Modelos</asp:LinkButton>

        <asp:LinkButton ID="lnkEstado" runat="server" onclick="lnkEstado_Click">Estados</asp:LinkButton>


        <asp:LinkButton ID="lnkKey" runat="server" onclick="lnkKey_Click" >Key</asp:LinkButton>

        <asp:LinkButton ID="lnkUnidad" runat="server" onclick="lnkUnidad_Click" >Unidades</asp:LinkButton>

        <asp:LinkButton ID="lnkSalir" runat="server" onclick="lnkSalir_Click"  >Salir</asp:LinkButton>


    </div>
    <br />
    <br />
    <br />