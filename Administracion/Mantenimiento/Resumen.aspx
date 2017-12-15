<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resumen.aspx.cs" Inherits="Administracion_Mantenimiento_Resumen" Title="Resumen de Mantenimiento" %>

<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">

<uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />

<h2>Resumen de Mantenimiento</h2>

<div class="datos-mantenimiento">
   <table>
      <tr>
        <td class="style1">Control</td>
        <td class="style2" style="text-align: left"><asp:Literal ID="ltrControl" runat="server"></asp:Literal></td>
        
        <td class="style7" style="text-align: left">Fecha de Solicitud</td>       
        <td class="style6" style="text-align: left"><asp:Literal ID="ltrSolicitud" runat="server"></asp:Literal></td> 
        
        <td class="style8">Solicidado Por</td>
        <td class="style5" style="text-align: left"><asp:Literal ID="ltrUsuario" runat="server"></asp:Literal></td>  
       </tr> 
       
       <tr>
        <td class="style1">Atendido por</td>
         <td class="style2" style="text-align: left"><asp:Literal ID="ltrResponsable" runat="server"></asp:Literal></td>  
        
         <td class="style7" style="text-align: left">Fecha Recepción</td>
         <td class="style6" style="text-align: left"><asp:Literal ID="ltrRecepcion" runat="server"></asp:Literal></td>  
 
         <td class="style8">Estado</td>
         <td class="style5" style="text-align: left"><asp:Literal ID="ltrEstado" runat="server"></asp:Literal></td>  
       </tr>
       
       <tr>
        <td class="style1">Descripción</td>
        <td colspan="5" style="text-align: left"><asp:Literal ID="ltrDescripcion" runat="server"></asp:Literal></td>                    
      </tr>
      
   </table>
    
</div>

<br />

<div class="datos-diagnosticos">

    <h2>Diagnosticos</h2>
          
    <asp:GridView ID="grdDiagnosticos" runat="server" AutoGenerateColumns="False" 
     EmptyDataText="No se encontro ningun registro" CellPadding="3" 
                            GridLines="Vertical" BackColor="White" 
        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" >
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
    
           <asp:BoundField DataField="revision" HeaderText="Revisión" 
           SortExpression="revision" />

          <asp:BoundField DataField="responsable" HeaderText="Responsable" 
           SortExpression="responsable" />
           
          <asp:BoundField DataField="descripcion" HeaderText="Descripción" 
           SortExpression="descripcion" />
    
        </Columns>

        <FooterStyle BackColor="#00376F" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#00376F" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    
</div>

    

<br />

<div class="datos-externos">

    <h2>Mantenimientos Externos</h2>
          
    <asp:GridView ID="grdExterno" runat="server" AutoGenerateColumns="False" 
     EmptyDataText="No se encontro ningun registro" CellPadding="3" 
        GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        BorderWidth="1px" style="margin-right: 0px; margin-top: 0px" >
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
    
           <asp:BoundField DataField="responsable" HeaderText="Responsable" 
           SortExpression="responsable" />
           
           <asp:BoundField DataField="envio" HeaderText="Envio" 
           SortExpression="envio" />
           
           <asp:BoundField DataField="descripcion" HeaderText="Observación de Envío" 
           SortExpression="descripcion" />
           
           <asp:BoundField DataField="recepcion" HeaderText="Recepción" 
           SortExpression="recepcion" />
           
           
          <asp:BoundField DataField="costo" HeaderText="Costo" 
           SortExpression="costo" />
 
           <asp:BoundField DataField="descripcionf" HeaderText="Diagnostico"   
           SortExpression="descripcionf" />          
    
        </Columns>

        <FooterStyle BackColor="#00376F" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#00376F" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    
    
</div>

</div>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">


    <link rel="icon" href="<%=ResolveUrl("~/favicon.ico")%>"  type="image/ico"/>

    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/ui-lightness/jquery-ui-1.8.23.custom.css")%>"  /> 
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/general.css")%>"  />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    
    <script src="<%=ResolveUrl("~/Util/js/jquery-1.8.0.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery-ui-1.8.23.custom.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery.ui.datepicker-es.js")%>" language="javascript" type="text/javascript"></script> 
	<script src="<%=ResolveUrl("~/Util/js/jquery.qtip-1.0.0-rc3.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/Script.js")%>" language="javascript" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.8.3.js" language="javascript" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js" language="javascript" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/Util/FancyBox/jquery.fancybox.js")%>"  language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/FancyBox/jquery.fancybox.css")%>" type="text/css" media="screen" />

    
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 92px;
        }
        .style2
        {
            width: 215px;
        }
        .style5
        {
            width: 157px;
        }
        .style6
        {
            width: 203px;
        }
        .style7
        {
            width: 122px;
        }
        .style8
        {
            text-align: left;
            width: 107px;
        }
    </style>



</asp:Content>


