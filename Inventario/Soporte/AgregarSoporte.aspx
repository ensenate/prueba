<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarSoporte.aspx.cs" Inherits="Matenimiento_Soporte_AgregarSoporte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de Soportes</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
    <div align="center">
   
   <h2 class="style1">Gestionar Soportes</h2> 
   
   <div id="detalleSoporte" align="center">
     <table border="0" style="width: 291px">
        <tr>
            <td>Nombre</td>
            <td><asp:Literal ID="ltrNombre" runat="server"></asp:Literal></td>  
        </tr>
        
        <tr>
            <td>Categoria</td>
            <td><asp:Literal ID="ltrCategoria" runat="server"></asp:Literal></td>  
        </tr>        
 
        <tr>
            <td>Marca</td>
            <td><asp:Literal ID="ltrMarca" runat="server"></asp:Literal></td>  
        </tr>

        <tr>
            <td>Modelo</td>
            <td><asp:Literal ID="ltrModelo" runat="server"></asp:Literal></td>  
        </tr>
        
        <tr>
            <td>Activo</td>
            <td><asp:Literal ID="ltrActivo" runat="server"></asp:Literal></td>  
        </tr>    
        
         <tr>
            <td>Descripcion</td>
            <td><asp:Literal ID="ltrDescripcion" runat="server"></asp:Literal></td>  
        </tr>              
        
     </table>
   </div>
      
    <br />  
    <br /> 
    <br /> 
  
   <div class="style1">
    
  <asp:LinkButton ID="btnAgregar" runat="server">Agregar 
        Soporte</asp:LinkButton>
     
        <br />
        <br />  
        </div>
    
  <asp:GridView ID="grdSoporte" runat="server" AutoGenerateColumns="False"  
     EmptyDataText="No se encontro ningun registro" 
      AllowPaging="True" PageSize="30"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                               

            <asp:BoundField DataField="Inicio" HeaderText="Fecha Envio" 
                SortExpression="Inicio" />
                
            <asp:BoundField DataField="Fin" HeaderText="Fecha Recepcion" 
                SortExpression="Fin" />
                
                
            <asp:BoundField DataField="Estado" HeaderText="Estado" 
                SortExpression="Estado" />     
                
                             
            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" 
                SortExpression="Ubicacion" />
                
            <asp:BoundField DataField="Responsable" HeaderText="Responsable" 
                SortExpression="Responsable" />
                                                                            
                   
            <asp:BoundField DataField="Observacioni" HeaderText="Obs. Envio" 
                SortExpression="Observacion" />          
              
             <asp:BoundField DataField="Observacionf" HeaderText="Obs. Recepcion" 
                SortExpression="Observacion" />       
               
             <asp:BoundField DataField="soporte" HeaderText="Soporte" 
                SortExpression="soporte" />   
                
                
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
       </div>
    </form>
</body>
</html>