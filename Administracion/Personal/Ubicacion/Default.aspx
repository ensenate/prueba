<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Matenimiento_Ubicacion_Default" %>

<%@ Register src="../../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
    
        
        <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
        <h2>
            Gestionar Ubicacion</h2>
    
       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
  
        
  <br />
  <br />
  
  <asp:GridView ID="grdUbicacion" runat="server" AutoGenerateColumns="False"  
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdUbicacion_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                 <asp:TemplateField HeaderText="Nombre" ShowHeader="False" SortExpression="name" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editUbicacion" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        
            <asp:BoundField DataField="descripcion" HeaderText="Descripcion" 
                SortExpression="descripcion" />
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
