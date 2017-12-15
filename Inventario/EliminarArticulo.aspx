<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EliminarArticulo.aspx.cs" Inherits="Inventario_EliminarArticulo" Title="Eliminar Articulo" %>
<%@ Register src="../CustomControls/MenuMantenimiento.ascx" tagname="MenuMantenimiento" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div align="center">
    
     <div>      
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtMotivo" 
             ErrorMessage="Debe ingresar una justificacion"  Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
      
    <div id="formularioArticuloEliminar">
    <br />
    <h2><asp:Literal ID="ltrArticulo" runat="server"></asp:Literal></h2>
    <h3>Motivo y Destino</h3>
        <asp:TextBox ID="txtMotivo" runat="server" Width="234px" Height="67px" 
            MaxLength="100" TextMode="MultiLine"></asp:TextBox>
      

<br />     
        
<br />  
<div id="btnForma">
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" 
                     onclick="btnCancelar_Click" /> </td></tr>
           </table>
 </div>                
                
        <br />
</div> 
    
    </div> 

</asp:Content>

