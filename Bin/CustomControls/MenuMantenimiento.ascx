<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuMantenimiento.ascx.cs" Inherits="CustomControls_MenuMantenimiento" %>

<div id="menu" align="center">
    
  
   <asp:LoginView ID="LoginView1" runat="server">
<RoleGroups>
<asp:RoleGroup Roles="admin">
<ContentTemplate>

        <asp:LinkButton ID="lnkInventario" runat="server" 
            PostBackUrl="~/Inventario/Default.aspx">Articulo</asp:LinkButton>
         

        <asp:LinkButton ID="lnkConsumo" runat="server" 
            PostBackUrl="~/Inventario/Desgaste/Default.aspx" >Consumo</asp:LinkButton>

        <asp:LinkButton ID="lnkPersonal" runat="server" 
            PostBackUrl="~/Administracion/Personal/Default.aspx">Personal</asp:LinkButton>

        <asp:LinkButton ID="lnkUbicacion" runat="server" PostBackUrl="~/Administracion/Ubicacion/Default.aspx">Ubicacion</asp:LinkButton>

        <asp:LinkButton ID="lnkEmpresa" runat="server" PostBackUrl="~/Administracion/Empresa/Default.aspx">Empresa</asp:LinkButton>

        <asp:LinkButton ID="lnkDominio" runat="server" PostBackUrl="~/Administracion/Dominio/Default.aspx" >Dominio</asp:LinkButton>

        <asp:LinkButton ID="lnkCuenta" runat="server" PostBackUrl="~/Administracion/Cuentas/Default.aspx" >Usuarios</asp:LinkButton>

        <asp:LinkButton ID="lnkSession" runat="server" onclick="lnkSession_Click" >Salir</asp:LinkButton>



</ContentTemplate>
 </asp:RoleGroup>
 
 <asp:RoleGroup Roles="superusuario">
<ContentTemplate>
        <asp:LinkButton ID="lnkInventario" runat="server" 
            PostBackUrl="~/Inventario/Default.aspx">Articulo</asp:LinkButton>
&nbsp;|

        <asp:LinkButton ID="lnkConsumo" runat="server" 
            PostBackUrl="~/Inventario/Desgaste/Default.aspx">Consumo</asp:LinkButton>
&nbsp;|

        <asp:LinkButton ID="lnkPersonal" runat="server" 
            PostBackUrl="~/Administracion/Personal/Default.aspx">Personal</asp:LinkButton>
&nbsp;|
        <asp:LinkButton ID="lnkUbicacion" runat="server" PostBackUrl="~/Administracion/Ubicacion/Default.aspx">Ubicacion</asp:LinkButton>
&nbsp;|
        <asp:LinkButton ID="lnkEmpresa" runat="server" PostBackUrl="~/Administracion/Empresa/Default.aspx">Empresa</asp:LinkButton>
&nbsp;|

        <asp:LinkButton ID="lnkDominio" runat="server" PostBackUrl="~/Administracion/Dominio/Default.aspx" >Dominio</asp:LinkButton>

&nbsp;|
        <asp:LinkButton ID="lnkSession" runat="server" onclick="lnkSession_Click" >Salir</asp:LinkButton>



</ContentTemplate>
 </asp:RoleGroup>
 
 <asp:RoleGroup Roles="usuario">
<ContentTemplate>
        <asp:LinkButton ID="lnkInventario" runat="server" 
            PostBackUrl="~/Inventario/Default.aspx">Articulo</asp:LinkButton>
&nbsp;|

        <asp:LinkButton ID="lnkConsumo" runat="server" 
            PostBackUrl="~/Inventario/Desgaste/Default.aspx">Consumo</asp:LinkButton>
&nbsp;|



        <asp:LinkButton ID="lnkSession" runat="server" onclick="lnkSession_Click" >Salir</asp:LinkButton>
</ContentTemplate>
 </asp:RoleGroup>
 
</RoleGroups>
    </asp:LoginView>
    
    </div>
    <br />
    <br />
    <br />