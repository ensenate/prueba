<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Inventario_Default"  Title="Listado de Articulo"%>


<%@ Register src="../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
    <div align="center">
   
        <uc2:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
    
 <h2>Articulos</h2>   
<div id ="busqueda-filtro">
<br />
   <asp:Panel ID="pnlFiltros" runat="server">
    <table border="0">
    
          <tr>
          
            <td><label for="lstCategoriaF">Categoria</label></td>
            <td><asp:DropDownList ID="lstCategoriaF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
            
 
             <td><label for="lstMarcaF">Marca</label></td>
            <td><asp:DropDownList ID="lstMarcaF" runat="server" Height="25px" Width="150px" onselectedindexchanged="lstMarcaF_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList></td>
            
             <td><label for="lstModeloF">Modelo</label></td>
            <td><asp:DropDownList ID="lstModeloF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>

         </tr>
         
        <tr>

            <td><label for="txtSerialF">Serial</label></td>
            <td><asp:TextBox ID="txtSerialF" runat="server"></asp:TextBox></td>
            
            <td><label for="txtControlF">Control</label></td>
            <td><asp:TextBox ID="txtControlF" runat="server"></asp:TextBox></td>
            
           <td><label for="lstEstadoF">En Uso</label></td>
            <td><asp:DropDownList ID="lstEstadoF" runat="server" Height="25px" Width="150px">
                </asp:DropDownList></td>
         
         </tr>   

         <tr>

            
            <td><label for="txtExtra1F">Extra1</label></td>
            <td><asp:TextBox ID="txtExtra1F" runat="server"></asp:TextBox></td>
            
             <td><label for="txtExtra2F">Extra2</label></td>
            <td><asp:TextBox ID="txtExtra2F" runat="server"></asp:TextBox></td>                                   
         
                      <td ><label for="lstKeywords" style="text-align: left">Keyword</label></td>
            <td><asp:DropDownList ID="lstKeywords" runat="server" Height="25px" 
                    Width="150px"> </asp:DropDownList></td>          
                   
        </tr>
        
        <tr>           
            <td><label for=lstEmpresa">Propietario</label></td>
            <td><asp:DropDownList ID="lstEmpresa" runat="server" Height="25px" Width="150px"> </asp:DropDownList></asp:TextBox></td>
                
            <td><label for="lstEliminados">Eliminados</label></td>
            <td><asp:DropDownList ID="lstEliminados" runat="server" Height="25px" Width="150px">
                <asp:ListItem Selected="True" Value="NO">No Mostrar</asp:ListItem>
                <asp:ListItem>Si Mostrar</asp:ListItem>
                <asp:ListItem>Ambos</asp:ListItem>
                </asp:DropDownList>
            </td>
            
             <td><label for="lstResponsable">Responsable</label></td>
              <td><asp:DropDownList ID="lstResponsable" runat="server" Height="25px" Width="150px">
                </asp:DropDownList>
            </td>                      
        </tr>
        
         <tr>           
            <td><label for=lstEstado">Estado</label></td>
            <td><asp:DropDownList ID="lstEstado" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
            
            <td><label for="lstEmpresa2">Empresa</label></td>
            <td><asp:DropDownList ID="lstEmpresa2" runat="server" Height="25px" Width="150px" onselectedindexchanged="lstEmpresa2_SelectedIndexChanged" AutoPostBack="True" >
                </asp:DropDownList>
            </td>
                
            <td><label for="lstUbicacion">Ubicacion</label></td>
            <td><asp:DropDownList ID="lstUbicacion" runat="server" Height="25px" Width="150px">
                </asp:DropDownList>
            </td>
                    
        </tr>
                
    </table>
    
    </asp:Panel>
    
    <div>
          <br />
          <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Imagenes/buscar.png"  onclick="btnGuardar_Click"/>
           <br />
           <br />
         </div>
         
   </div>

<br />

     <div align="center">
      <br />
       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
     </div>
     
        <br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        <br />
     
<br />

     
  <asp:GridView ID="grdArticulo" runat="server"  BackColor ="#507CD1"   
            CellSpacing="2"  AutoGenerateColumns="False"  EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdArticulo_RowCommand" 
      OnPageIndexChanging="GridView1_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="grdArticulo_SelectedIndexChanged" 
            onrowdatabound="grdArticulo_RowDataBound">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                           
                   <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton2" ToolTip="Asociar" runat="server" CommandName="relacionarArticulo" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/agregarp.png"  CssClass='<%# Eval("ocultarAsociar") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>    
                   
                    
                <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton1" ToolTip="Traslado" runat="server"  CommandName="historiaArticulo" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/mover.png" CssClass='<%# Eval("ocultarTraslado") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>
                    
                                 <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton3" ToolTip="Imprimir" runat="server" CommandName="imprimirEtiqueta" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/imprimir.png"  />
  
                    </ItemTemplate>
                </asp:TemplateField>       
 
                                
                <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton6" ToolTip="Eliminar" runat="server"  CommandName="eliminarArticulo" CommandArgument='<%# Eval("control") %>' ImageUrl="~/Imagenes/eliminarp.png"  OnClientClick="return confirm('¿Desea eliminar el registro?');"  CssClass='<%# Eval("ocultarEliminar") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Control" ShowHeader="False" SortExpression="control" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" ToolTip="Editar"  runat="server" CausesValidation="false" 
                         CommandName="editArticulo" Text='<%# Eval("control") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>

                                                              
            <asp:BoundField DataField="activo" HeaderText="Activo" 
                SortExpression="activo" /> 
                
            <asp:BoundField DataField="empresa" HeaderText="Empresa" 
                SortExpression="empresa" /> 
                
            <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion" 
                SortExpression="ubicacion" /> 

            <asp:BoundField DataField="contenidos" HeaderText="Articulos" 
                SortExpression="contenidos" /> 


            <asp:BoundField DataField="pertenece" HeaderText="Pertenece" 
                SortExpression="pertenece" /> 
                
            <asp:BoundField DataField="categoria" HeaderText="Categoria" 
                SortExpression="categoria" />
                
            <asp:BoundField DataField="marca" HeaderText="Marca" 
                SortExpression="marca" />
                
            <asp:BoundField DataField="modelo" HeaderText="Modelo" 
                SortExpression="modelo" />         
                
            <asp:BoundField DataField="serial" HeaderText="Serial" 
                SortExpression="serial" />
            <asp:BoundField DataField="extra1" HeaderText="Extra1" 
                SortExpression="extra1" />

            <asp:BoundField DataField="extra2" HeaderText="Extra2" 
                SortExpression="extra2" />

 
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
     
    </div>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">


    <link rel="icon" href="<%=ResolveUrl("~/favicon.ico")%>"  type="image/ico"/>

    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/ui-lightness/jquery-ui-1.8.23.custom.css")%>"  /> 
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/general.css")%>"  />
    
    <script src="<%=ResolveUrl("~/Util/js/jquery-1.8.0.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery-ui-1.8.23.custom.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/Script.js")%>" language="javascript" type="text/javascript"></script>
   
    

</asp:Content>

