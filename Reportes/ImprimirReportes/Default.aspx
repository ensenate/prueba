<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="ImprimirReporte" %>


<%@ Register src="~/CustomControls/MenuReporte.ascx" tagname="MenuMantenimiento" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     
 <div align="center" runat="server">
       <uc2:MenuMantenimiento ID="MenuReporte1" runat="server" />


                
<div id="busqueda-filtros" >
<h2>Filtrar para obtener el reporte</h2>

<asp:Panel ID="pnlFiltros" runat="server">
    <table border="0">

	 	<tr>           
            <td><label for="lstEmpresa2">Empresa</label></td>
            <td><asp:DropDownList ID="lstEmpresa2" runat="server" Height="25px" onselectedindexchanged="lstEmpresa2_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>     
            
            <td><label for="lstUbicacion">Ubicacion</label></td>
            <td><asp:DropDownList ID="lstUbicacion" runat="server" Height="25px" >
                </asp:DropDownList>
            </td>

         	<td><label for="lstResponsable">Responsable</label></td>
              <td><asp:DropDownList ID="lstResponsable" runat="server" Height="25px" >
                </asp:DropDownList>
            </td>
                    
        </tr>

		<tr>

			<td><label for="lstCategoriaF">Categoria</label></td>
			<td><asp:DropDownList ID="lstCategoriaF" runat="server" Height="25px" > </asp:DropDownList></td>


			<td><label for="lstMarcaF">Marca</label></td>
			<td><asp:DropDownList ID="lstMarcaF" runat="server" Height="25px" onselectedindexchanged="lstMarca_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList></td>

			<td><label for="lstModeloF">Modelo</label></td>
			<td><asp:DropDownList ID="lstModeloF" runat="server" Height="25px" > </asp:DropDownList></td>

		</tr>
         
        <tr>

            <td><label for="txtSerialF">Serial</label></td>
            <td><asp:TextBox ID="txtSerialF" runat="server"></asp:TextBox></td>
            
            <td><label for="txtControlF">Control</label></td>
            <td><asp:TextBox ID="txtControlF" runat="server"></asp:TextBox></td>
            
           <td><label for="lstEstadoF">En Uso</label></td>
            <td><asp:DropDownList ID="lstEstadoF" runat="server" Height="25px" >
                </asp:DropDownList></td>
         
         </tr>   

         <tr>

            
            <td><label for="txtExtra1F">Extra1</label></td>
            <td><asp:TextBox ID="txtExtra1F" runat="server"></asp:TextBox></td>
            
            <td><label for="txtExtra2F">Extra2</label></td>
            <td><asp:TextBox ID="txtExtra2F" runat="server"></asp:TextBox></td> 

            <td><label for="lstEliminados">Eliminados</label></td>
            <td><asp:DropDownList ID="lstEliminados" runat="server" Height="25px" ></asp:DropDownList>
            </td>
                   
        </tr>
        
        <tr>
        	<td><label for=lstEstado">Estado</label></td>
            <td><asp:DropDownList ID="lstEstado" runat="server" Height="25px" > </asp:DropDownList></td>
                                  
        </tr>
        
        
                
    </table>
    
    </asp:Panel>

	
	<div align="center">
		<br />
		<asp:ImageButton ID="btnReporteFiltro" runat="server" ImageUrl="~/Imagenes/reporte-completo.png"  onclick="btnReporteFiltro_Click"/>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
	</div>




    </div>
</div> 
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">


    <link rel="icon" href="<%=ResolveUrl("~/favicon.ico")%>"  type="image/ico"/>

    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/ui-lightness/jquery-ui-1.8.23.custom.css")%>"  /> 
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/imprimir-reportes.css")%>"  />
    
    <script src="<%=ResolveUrl("~/Util/js/jquery-1.8.0.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery-ui-1.8.23.custom.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/Script.js")%>" language="javascript" type="text/javascript"></script>
   
    

</asp:Content>


