<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Matenimiento_Dominio_Default" Title="Listado de Dominios"  EnableEventValidation="true" %>

<%@ Register src="../../CustomControls/MenuDominios.ascx" tagname="MenuDominios" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div align="center">

    <uc1:MenuDominios ID="MenuDominios1" runat="server" />
    <h2>
    <asp:Literal ID="ltrH2" runat="server"></asp:Literal></h2>
    <div>
    
    
     <div>
      <br />
       <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/agregar.png"  onclick="LinkButton1_Click1"/>
     </div>
  
        <br />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <br />
        <br />
        
        
          <asp:GridView ID="grdDominio" runat="server" AutoGenerateColumns="False"   BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      onrowcommand="grdDominio_RowCommand" 
      OnPageIndexChanging="grd_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>


                
                  <asp:TemplateField HeaderText="" ShowHeader="False" SortExpression="nombre" >
                    <ItemTemplate>
  
                     <asp:ImageButton ID="ImageButton1" runat="server" CommandName="eliminarDominio" CommandArgument='<%# Eval("id") %>' ImageUrl="~/Imagenes/eliminarp.png" OnClientClick="return confirm('¿Desea eliminar el registro?');"  />
  
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="Nombre" ShowHeader="true" SortExpression="nombre" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                            CommandName="editDominio" Text='<%# Eval("nombre") %>' CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>      
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
