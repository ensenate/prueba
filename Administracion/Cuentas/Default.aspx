<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administracion_Cuentas_Default" Title= "Listado de Cuentas"  %>

<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
    
        <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
        <h2>
            Gestionar Usuarios</h2>
    

     <div>
      <br />
       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="btnAgregar_Click"/>
     </div>
        
  <br />
  <br />
 
  
  <asp:GridView ID="grdCuentas" runat="server" AutoGenerateColumns="False"   BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdCuentas_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                 <asp:TemplateField HeaderText="Cuenta" ShowHeader="False" SortExpression="cuenta" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editCuenta" Text='<%# Eval("usuario") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        
            <asp:BoundField DataField="responsable" HeaderText="Responsable" 
                SortExpression="responsable" />
                                
            <asp:BoundField DataField="rol" HeaderText="Rol" 
                SortExpression="rol" />

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