<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Desgaste_Default" Title="Consumo por Articulo" %>

<%@ Register src="../../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
         <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
         <h2>
             Consumo por Articulo</h2>
    
     <br />     
     
          <asp:ImageButton ID="btnAgregar"  onclick="btnAgregar_Click" runat="server" ImageUrl="~/Imagenes/agregar.png"  />
     
     
     
         &nbsp;     
     
        <br />
  <br />
  
  <asp:GridView ID="grdArticulo" runat="server" AutoGenerateColumns="False"    BackColor="#507CD1"   CellSpacing="2" 
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10"
      OnPageIndexChanging="grdArticulo_PageIndexChanging"
            CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>

               <asp:BoundField DataField="cantidadAnterior" HeaderText="Existencia Anterior"  ItemStyle-HorizontalAlign="Right"  />
                 
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"  />  
                 
               <asp:BoundField DataField="cantidadActual" HeaderText="Existencia Actual" ItemStyle-HorizontalAlign="Right"  />
       
               <asp:BoundField DataField="fecha" HeaderText="Fecha"  />
               
               <asp:BoundField DataField="responsable" HeaderText="Responsable"  />
               
                <asp:BoundField DataField="empresa" HeaderText="Empresa"  />
               
               <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion"  />
       
               <asp:BoundField DataField="obs" HeaderText="Observacion" SortExpression="obs" />  
 
               <asp:BoundField DataField="unidad" HeaderText="Unidad" 
                SortExpression="unidad" />       
                
              <asp:BoundField DataField="usuario" HeaderText="Usuario" 
                SortExpression="usuario" />
     

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

