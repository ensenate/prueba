<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscarArticulo.ascx.cs" Inherits="CustomControls_BuscarArticulo" %>


<script language=javascript type="text/javascript">

	function traerValor(valor){
	    document.getElementById("ctl00_ContentPlaceHolder1_txtControl").value=valor;
	}
	
	
</script>


<div style="display:none">
   
    <div id="myDivID"   align="center" >

   <div >
       <h2>Buscar Articulo</h2>
       <asp:Label ID="Label1" runat="server" Text="Buscar" AssociatedControlID="txtBuscar" ></asp:Label>
       <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
   </div> 
 
   <br />
 
  <asp:GridView ID="grdArticulo" runat="server"  BackColor ="#507CD1" class="filtrar"   
            CellSpacing="2"  AutoGenerateColumns="False"  EmptyDataText="No se encontro ningun registro" 
      AllowPaging="false"
      onrowcommand="grdArticulo_RowCommand" 
      OnPageIndexChanging="GridView1_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="grdArticulo_SelectedIndexChanged" 
            onrowdatabound="grdArticulo_RowDataBound">
        <RowStyle BackColor="#EFF3FB"  />
        <Columns>        
   


          <asp:TemplateField HeaderText="Control" ShowHeader="False" SortExpression="control"  >
              <ItemTemplate>
                 <asp:LinkButton ID="lnkSeleccionar" ToolTip="Seleccionar"  runat="server" CausesValidation="false" 
                        CommandName="seleccionar" Text='<%# Eval("control") %>' CommandArgument='<%# Eval("control") %>'></asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
                
                
             <asp:BoundField DataField="serial" HeaderText="Serial" 
                SortExpression="serial" />
                
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


