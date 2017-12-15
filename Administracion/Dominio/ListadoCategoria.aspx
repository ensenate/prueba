<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListadoCategoria.aspx.cs" Inherits="Administracion_Categoria_Default" Title="Listado de Categorias" %>


<%@ Register src="../../CustomControls/MenuDominios.ascx" tagname="MenuDominios" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center">
    
       
        <uc1:MenuDominios ID="MenuDominios1" runat="server" />
        <h2>
            Gestionar Categoria</h2>

       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
  
        
  <br />
  <br />
  
  <asp:GridView ID="grdCategoria" runat="server" AutoGenerateColumns="False"  BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdCategoria_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                        <asp:TemplateField HeaderText="Nombre" ShowHeader="False" SortExpression="name" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editCategoria" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
      
            
               <asp:BoundField DataField="prefijo" HeaderText="Prefijo" 
                SortExpression="prefijo" />
                
                <asp:BoundField DataField="ejemplo1" HeaderText="Ejemplo 1" 
                SortExpression="ejemplo1" />
  
                  <asp:BoundField DataField="ejemplo2" HeaderText="Ejemplo 2" 
                SortExpression="ejemplo2" />
   
                
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