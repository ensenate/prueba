<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuReporte.ascx.cs" Inherits="CustomControls_MenuReporteç"  %>


<div id="menu" align="center">
    
  
   <asp:LoginView ID="LoginView1" runat="server">
<RoleGroups>
<asp:RoleGroup Roles="admin">
<ContentTemplate>


        <asp:LinkButton ID="lnkInventario" runat="server" 
            OnClick="ver_articulos_Click">Pag. Principal</asp:LinkButton>
         
        <asp:LinkButton ID="LnkArticulos" runat="server" onclick="LnkArticulos" >Art. Retirados</asp:LinkButton>
        <asp:LinkButton ID="LnkEtiquetas" runat="server" onclick="LnkEtiquetas_Click"  >Impresion de Etiquetas</asp:LinkButton>
        <asp:LinkButton ID="LnkReportes" runat="server" onclick="lnkReporte_Click" >Listado de Asociaciones</asp:LinkButton>
        <asp:LinkButton ID="LnkImprimirReportes" runat="server" onclick="LnkImprimirReportes" >Listado Reportes</asp:LinkButton>



</ContentTemplate>
 </asp:RoleGroup>

 
</RoleGroups>
    </asp:LoginView>
    
    </div>
    <br />
    <br />
    <br />