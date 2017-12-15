<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administracion_Mantenimiento_Default"  Title= "Listado de Mantenimientos" %>

<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">

<uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />

  <h2>Listado de Mantenimientos</h2>
  
  
  <div class="filtro-busqueda-mantenimiento" >
   
   <table border="0">
      <tr>
       <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label4" runat="server" Text="Articulo" AssociatedControlID="txtArticulo" ></asp:Label> </td>         
        <td>  <asp:TextBox ID="txtArticulo" runat="server" Width="80"  ></asp:TextBox></td>  
        <td> <asp:Label ID="Label2" runat="server" Text="Categoria" AssociatedControlID="lstCategoria" ></asp:Label> </td>         
        <td><asp:DropDownList ID="lstCategoria" runat="server" Width="150"> </asp:DropDownList></td>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label1" runat="server" Text="Estado" AssociatedControlID="lstEstado" ></asp:Label> </td>         
        <td><asp:DropDownList ID="lstEstado" runat="server" Width="150"> </asp:DropDownList></td>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label3" runat="server" Text="Fecha Solicitud" AssociatedControlID="txtInicio" ></asp:Label> </td>         
        <td>  <asp:TextBox ID="txtInicio" runat="server" CssClass="datepickers" Width="80"  ></asp:TextBox></td>  
        <td>  &nbsp;<asp:TextBox ID="txtFin" runat="server" CssClass="datepickers" Width="80"  ></asp:TextBox></td>  
        <td> &nbsp; <asp:ImageButton ID="btnBuscar" runat="server" 
                ImageUrl="~/Imagenes/buscar.png" Height="37px" onclick="btnBuscar_Click" 
                Width="44px" /></td>
     </tr>     
     
       
     
  </table>
  
  </div>
   
   <br />
    
  <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png" 
        onclick="btnAgregar_Click"  />
  
  <br /> <br />
  
   <asp:GridView ID="grdArticulo" runat="server"  BackColor ="#507CD1"   
            CellSpacing="2"  AutoGenerateColumns="False"  EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdArticulo_RowCommand" 
      OnPageIndexChanging="GridView1_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None" >
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                                            
                <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton2" ToolTip="Dar Recepcion" runat="server"  CommandName="Recepcion" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/calendariop.png" CssClass='<%# Eval("ocultarRecepcion") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>
   
                 <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton8" ToolTip="Cerrar Soporte Externo" runat="server"  CommandName="ExternoCerrar" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/repararc.png" CssClass='<%# Eval("ocultarExternoCerrado") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                   <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton6" ToolTip="Abrir Soporte Externo" runat="server"  CommandName="ExternoAbrir" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/reparar.png" CssClass='<%# Eval("ocultarExternoAbrir") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>
                             
                <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton1" ToolTip="Resuelto" runat="server"  CommandName="Resuelto" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/reporte.png"  CssClass='<%# Eval("ocultarResuelto") %>' />
  
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                       <asp:ImageButton ID="ImageButton3" ToolTip="Realizar Diagnostico" runat="server" CommandName="Diagnostico" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/diagnostico.png" CssClass='<%# Eval("ocultarDiagnostico") %>'  />
  
                    </ItemTemplate>
                </asp:TemplateField>       
                 
                 <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                       <asp:ImageButton ID="ImageButto6" ToolTip="Ver" runat="server" CommandName="Ver" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/buscar.png" Height="25" Width="25"  />
  
                    </ItemTemplate>
                </asp:TemplateField>       
                                              
                
                <asp:BoundField DataField="id" HeaderText="Número" 
                SortExpression="id" />

               <asp:TemplateField HeaderText="Control"  SortExpression="control" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkVerArticulo" ToolTip="Ver Articulo"  runat="server" CausesValidation="false" 
                         CommandName="Articulo" Text='<%# Eval("control") %>' CommandArgument='<%# Eval("idArticulo") %>'></asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>


              <asp:BoundField DataField="categoria" HeaderText="Categoria" 
                SortExpression="categoria" />
                
            <asp:BoundField DataField="marca" HeaderText="Marca" 
                SortExpression="marca" />
                
            <asp:BoundField DataField="modelo" HeaderText="Modelo" 
                SortExpression="modelo" />         
                
            <asp:BoundField DataField="empresa" HeaderText="Empresa" 
                SortExpression="empresa" />     

            <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion" 
                SortExpression="ubicacion" />    

            <asp:BoundField DataField="responsablea" HeaderText="Responsable" 
                SortExpression="responsablea" />    
                                                
            <asp:BoundField DataField="serial" HeaderText="Serial" 
                SortExpression="serial" />
                
                                                              
            <asp:BoundField DataField="fechaSolicitud" HeaderText="Solicitud" 
                SortExpression="fechaSolicitud" /> 
  
  
              <asp:BoundField DataField="descripcion" HeaderText="Descripción" 
                SortExpression="descripcion" />               
 
               <asp:BoundField DataField="responsable" HeaderText="Atendido Por" 
                SortExpression="responsable" />                     
 
                <asp:BoundField DataField="estado" HeaderText="Estado" 
                SortExpression="estado" />          
                
                <asp:BoundField DataField="fechaRecibido" HeaderText="Recibido" 
                SortExpression="fechaRecibido" />    
                
                <asp:BoundField DataField="usuario" HeaderText="Solicitado Por" 
                SortExpression="usuario" />  
                
                <asp:BoundField DataField="fechaEnvio" HeaderText="Fecha Envío" 
                SortExpression="fechaEnvio" />        
                 
  
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