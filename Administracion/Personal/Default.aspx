<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Matenimiento_Personal_Default" Title="Listado de Personal" %>

<%@ Register src="../../CustomControls/MenuDominios.ascx" tagname="MenuDominios" tagprefix="uc1" %>
<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="center">
    
       <uc2:MenuMantenimiento ID="MenuMantenimiento2" runat="server" />
        <h2>
            Personal Interno</h2>
    
       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
  
        
        
  <br />
  <br />
  
  <asp:GridView ID="grdPersonal" runat="server" AutoGenerateColumns="False"  BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdPersonal_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                        <asp:TemplateField HeaderText="Nombre" ShowHeader="true" SortExpression="nombre" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editPersonal" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        

            <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                SortExpression="telefono" />
            <asp:BoundField DataField="correo" HeaderText="Correo" 
                SortExpression="correo" />
                
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


<br />
<hr />
<h2>Personal Externo</h2>

  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
  <br />
  <br />

 <asp:GridView ID="grdPersonal2" runat="server" AutoGenerateColumns="False"  BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdPersonal_RowCommand2" 
      OnPageIndexChanging="grd_PageIndexChanging2"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                        <asp:TemplateField HeaderText="Nombre" ShowHeader="true" SortExpression="nombre" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editPersonal" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            <asp:BoundField DataField="rif" HeaderText="Rif" 
                SortExpression="rif" />
                
            <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                SortExpression="telefono" />
            <asp:BoundField DataField="correo" HeaderText="Correo" 
                SortExpression="correo" />
                
      
                
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