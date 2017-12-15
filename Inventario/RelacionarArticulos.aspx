<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RelacionarArticulos.aspx.cs" Inherits="Inventario_relacionarArticulos" Title="Asociar Articulos" %>

<%@ Register src="../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
   <asp:HiddenField  ID="trasladarA" runat="server" Value="S" />
  <div id="pertenece">
            <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
        <h2>
            Elementos Contenidos en el Control <asp:Literal ID="ltrArticulo" runat="server"></asp:Literal></h2>
            <p> 
                <asp:Label ID="lblMensajeControl" runat="server" ForeColor="Red"></asp:Label>
            </p> 
             <asp:Label ID="Label1" runat="server" Text="Control" 
                style="text-align: left; font-weight: 700"></asp:Label>
                &nbsp;<asp:TextBox ID="txtItem" runat="server" Height="22px"></asp:TextBox>&nbsp;
                <asp:ImageButton  ID="btnAgregar" runat="server"   
                ImageUrl="~/Imagenes/agregar.png" Height="21px" 
                Width="28px" onclick="btnAgregar_Click"  />
                
            <br /> <br /> 
             
            <asp:GridView ID="grdArticulo" runat="server" AutoGenerateColumns="False"  
                            BackColor="#507CD1"   CellSpacing="2"   EmptyDataText="No se encontro ningun registro" 
              AllowPaging="True" PageSize="10"
              CellPadding="4" ForeColor="#333333" GridLines="None" 
              onrowcommand="grdArticulo_RowCommand"
              OnPageIndexChanging="grdArticulo_PageIndexChanging" 
                            onselectedindexchanged="grdArticulo_SelectedIndexChanged">
            <RowStyle BackColor="#EFF3FB" />
        <Columns>
                           
             <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server"  OnClientClick="return confirm('¿Desea eliminar el registro?');"  CommandName="eliminarArticulo" CommandArgument='<%# Eval("control") %>' ImageUrl="~/Imagenes/eliminarp.png" />
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
    
  
  <br />
  <br />
    
 <div id ="divfiltro">    
     <br />

    <table border="0">
        <tr>
        
            <td><label for="lstEstadoF">Activo</label></td>
            <td class="style2"><asp:DropDownList ID="lstEstadoF" runat="server" Height="25px" Width="150px">
                </asp:DropDownList></td>
        
            <td><label for="lstCategoriaF" style="text-align: right">Categoria</label></td>
            <td><asp:DropDownList ID="lstCategoriaF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
            
            <td rowspan="3">&nbsp;<asp:ImageButton ID="btnBuscar" runat="server" 
                    ImageUrl="~/Imagenes/buscar.png"  onclick="btnGuardar_Click" Height="55px" 
                    Width="52px"/>
                                </td>
         </tr>   

         <tr>
            
            <td class="style1"><label for="lstMarcaF" style="text-align: left">Marca</label></td>
            <td><asp:DropDownList ID="lstMarcaF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
                         
                         
            <td class="style1"><label for="lstModeloF">Modelo</label></td>
            <td class="style2"><asp:DropDownList ID="lstModeloF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
                         
                   
        </tr>



          <tr>
          
   
            <td><label for="txtExtra1">Extra 1</label></td>
            <td class="style2"><asp:TextBox ID="txtExtra1" runat="server" Width="145px" Height="22px"></asp:TextBox></td>
            
            <td class="style1"><label for="txtExtra2">Extra 2</label></td>
            <td><asp:TextBox ID="txtExtra2" runat="server" Width="145px" Height="22px"></asp:TextBox></td>


         </tr>
         
         <tr>
            <td><label for="txtControl">Control</label></td>
            <td><asp:TextBox ID="txtControl" runat="server" Width="145px" Height="22px"></asp:TextBox></td>
         </tr>
                
    </table>

    <div>
          <br />
         
           <br />
           <br />
    </div>
         
</div>

  <div id="todosArticulos" >
  
    <h3>Articulos Disponibles Para Agregar</h3>
   
            
                    <asp:GridView ID="grdArticulo2" runat="server"  BackColor="#507CD1"   CellSpacing="2"  AutoGenerateColumns="False"  EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
    
        CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowcommand="grdArticulo2_RowCommand"
                OnPageIndexChanging="grdArticulo2_PageIndexChanging"
                >
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                       
                 <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
                      <asp:ImageButton ID="ImageButton1" runat="server" CommandName="agregarArticulo" CommandArgument='<%# Eval("control") %>' ImageUrl="~/Imagenes/agregarp.png"  CssClass="pruebac"/>
                    </ItemTemplate>
                </asp:TemplateField>
          
                <asp:TemplateField HeaderText="Control" ShowHeader="False" SortExpression="control" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar2" ToolTip="Editar"  runat="server" CausesValidation="false" 
                         CommandName="editArticulo" Text='<%# Eval("control") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField> 
                                                              
            <asp:BoundField DataField="activo" HeaderText="Activo" 
                SortExpression="activo" /> 
                
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

</div>
 
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">


    <link rel="icon" href="<%=ResolveUrl("~/favicon.ico")%>"  type="image/ico"/>

    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/ui-lightness/jquery-ui-1.8.23.custom.css")%>"  /> 
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/general.css")%>"  />
    
    <script src="<%=ResolveUrl("~/Util/js/jquery-1.8.0.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery-ui-1.8.23.custom.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/Script.js")%>" language="javascript" type="text/javascript"></script>

    
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style2
        {
            width: 161px;
        }
    </style>



</asp:Content>


