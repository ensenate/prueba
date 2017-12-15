<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListadoModelo.aspx.cs" Inherits="Administracion_Dominio_ListadoModelo" Title="Modelos" %>


<%@ Register src="../../CustomControls/MenuDominios.ascx" tagname="MenuDominios" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="center">
    
       
       
        <uc1:MenuDominios ID="MenuDominios1" runat="server" />
        <h2>
            Gestionar Modelo</h2>

       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
  
        
        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
  
        
  <br />
  <br />
  
  <asp:GridView ID="grdModelo" runat="server" AutoGenerateColumns="False"  BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdModelo_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
        
                        
                  <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton1" runat="server" CommandName="eliminarModelo" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/eliminarp.png" OnClientClick="return confirm('¿Desea eliminar el registro?');"  />
  
                    </ItemTemplate>
                </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="Nombre" ShowHeader="False" SortExpression="name" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editModelo" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
      
            
               <asp:BoundField DataField="marca" HeaderText="Marca" 
                SortExpression="macar" />
   
                
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

