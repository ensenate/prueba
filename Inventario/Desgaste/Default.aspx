<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Desgaste_Default" Title="Listado de Consumos" %>



<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
    
     
       
        <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
        <h2>
            Gestionar Articulo Consumibles</h2>

    
         <div align="center">
         
         <div id ="busqueda-filtro">
<br />
    <table border="0">
    
          <tr>
          
            <td><label for="lstCategoriaF">Categoria</label></td>
            <td><asp:DropDownList ID="lstCategoriaF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
            
 
             <td><label for="lstMarcaF">Marca</label></td>
            <td><asp:DropDownList ID="lstMarcaF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>
            
             <td><label for="lstModeloF">Modelo</label></td>
            <td><asp:DropDownList ID="lstModeloF" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>

         </tr>
         
        <tr>           
            <td><label for="txtControlF">Control</label></td>
            <td><asp:TextBox ID="txtControlF" runat="server"></asp:TextBox></td>
         
            <td><label for="lstEmpresa">Empresa</label></td>
            <td><asp:DropDownList ID="lstEmpresa" runat="server" Height="25px" Width="150px"> </asp:DropDownList></td>         
   
            <td><label for="lstDescontinuado">Descontinuado</label></td>
            <td><asp:DropDownList ID="lstDescontinuado" runat="server" Height="25px" Width="150px">
                <asp:ListItem Value="1">Si</asp:ListItem>
                <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                <asp:ListItem Value="0">Ambos</asp:ListItem>
                </asp:DropDownList></td>         
                       
                                          
         </tr>   

                
    </table>
    
    <div>
          <br />
          <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Imagenes/buscar.png" 
              onclick="btnBuscar_Click" />
           <br />
           <br />
         </div>
         
   </div>
   
   
      <br />
       <asp:ImageButton ID="btnAgregar"  onclick="btnAgregar_Click" runat="server" ImageUrl="~/Imagenes/agregar.png"  />
     </div>
  <br />
  <br />
  
  <asp:GridView ID="grdArticulo" runat="server" AutoGenerateColumns="False"   BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdArticulo_RowCommand" 
      OnPageIndexChanging="grdArticulo_PageIndexChanging"
      
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
 
                  <asp:TemplateField HeaderText=""   >
                    <ItemTemplate>
   <asp:ImageButton ID="ImageButton3" ToolTip="Descontinuar" runat="server" CommandName="descontinuar"  CommandArgument='<%# Eval("id") %>'  ImageUrl="~/Imagenes/eliminarp.png"  CssClass='<%# Eval("ocultarEliminar") %>' OnClientClick="return confirm('¿Desea Descontinuar el Articulo?');"  />
                    </ItemTemplate>
                </asp:TemplateField>
                    
                <asp:TemplateField HeaderText=""   >
                   <ItemTemplate>
   <asp:ImageButton ID="ImageButton2" ToolTip="Realizar Conteo" runat="server" CommandName="contarConsumibles"  CommandArgument='<%# Eval("id") %>'  ImageUrl="~/Imagenes/conteo.png"  />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText=""   >
                    <ItemTemplate>
   <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Editar Existencia" CommandName="editExistencia"  CommandArgument='<%# Eval("id") %>'  ImageUrl="~/Imagenes/mover.png"  />
                    </ItemTemplate>
                </asp:TemplateField>
                

                 <asp:TemplateField HeaderText="Control" ShowHeader="true"  >
                    <ItemTemplate>            
                            <asp:LinkButton ID="lnkArticulo" runat="server" CausesValidation="false" 
                            CommandName="editArticulo" Text='<%# Eval("control") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

   
               <asp:BoundField DataField="descripcion" HeaderText="Descripcion" 
                SortExpression="cantidad" />
       
                   <asp:BoundField DataField="cantidad" HeaderText="Cantidad" 
                SortExpression="cantidad" />
       
                   <asp:BoundField DataField="unidad" HeaderText="Unidad" 
                SortExpression="unidad" />  
                

                
           <asp:BoundField DataField="categoria" HeaderText="Categoria" 
                SortExpression="categoria" />
                
            <asp:BoundField DataField="marca" HeaderText="Marca" 
                SortExpression="marca" />
                
            <asp:BoundField DataField="modelo" HeaderText="Modelo" 
                SortExpression="modelo" />         

            <asp:BoundField DataField="empresa" HeaderText="Empresa" 
                SortExpression="empresa" />     
                    
       
                

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

