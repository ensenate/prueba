<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Inventario_Historia_Default" Title="Movimiento de Articulos" %>

<%@ Register src="../../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div align="center">
   
        <uc1:MenuMantenimiento ID="MenuMantenimiento1" runat="server" />
        
           <h2>Gestionar Movimientos</h2>
   <div id="formularioArticulo">
   <br />

     <table border="0" style="width: 344px">
  
        <tr>
            <td style="text-align: right">Categoria:</td>
            <td style="text-align: left" colspan="3"><asp:Literal ID="ltrCategoria" runat="server"></asp:Literal></td>  
        </tr>  

        <tr>
            <td style="text-align: right">Modelo:</td>
            <td style="text-align: left"><asp:Literal ID="ltrModelo" runat="server"></asp:Literal></td>  
             <td style="text-align: right">Marca:</td>
            <td style="text-align: left"><asp:Literal ID="ltrMarca" runat="server"></asp:Literal></td>
        </tr>
        
        <tr>
            <td style="text-align: right">Control:</td>
            <td style="text-align: left"><asp:Literal ID="ltrControl" runat="server"></asp:Literal></td> 
           <td style="text-align: right">Activo:</td>
            <td style="text-align: left"><asp:Literal ID="ltrActivo" runat="server"></asp:Literal></td>               
        </tr>    
        
            
        
     </table>

    <br />
    
</div>       
    <br />  
    <br /> 
    <br /> 
  
   <div>
    
    <div>
         <asp:ImageButton ID="btnAgregar"  onclick="btnAgregar_Click" runat="server" ImageUrl="~/Imagenes/agregar.png"  />
    </div>
     
        <br />
        <br />  
        </div>
    
  <asp:GridView ID="grdHistorial"   BackColor="#507CD1"   CellSpacing="2"  BorderWidth="1" runat="server"  AutoGenerateColumns="False"  
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="10" 
      OnPageIndexChanging="grdHistorial_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                               

            <asp:BoundField DataField="Inicio" HeaderText="Asignado" 
                SortExpression="Inicio" />
                
            <asp:BoundField DataField="Fin" HeaderText="Fin" Visible="False" 
                SortExpression="Fin" />
                
                
            <asp:BoundField DataField="Estado" HeaderText="Estado" 
                SortExpression="Estado" />     
                
  
              <asp:BoundField DataField="Empresa" HeaderText="Empresa" 
                SortExpression="Empresa" />
                                           
            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" 
                SortExpression="Ubicacion" />
                
            <asp:BoundField DataField="Responsable" HeaderText="Responsable" 
                SortExpression="Responsable" />
 
             <asp:BoundField DataField="Autorizado" HeaderText="Autorizado" 
                SortExpression="Autorizado" />
                
            <asp:BoundField DataField="Realizado" HeaderText="Realizado" 
                SortExpression="Realizado" />
                
            <asp:BoundField DataField="Observacion" HeaderText="Obs" 
                SortExpression="Observacion" />          
               
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