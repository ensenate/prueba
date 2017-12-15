<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConteoConsumibles.aspx.cs" Inherits="Inventario_Desgaste_Existencia_ConteoConsumibles" Title="Consumo del Articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   <div align="center">
   
  
            <div>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtCantidad" Display="Dynamic" 
                        ErrorMessage="La cantidad es obligatoria"></asp:RequiredFieldValidator>
                <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtObs" Display="Dynamic" 
                        ErrorMessage="La Observacion es Requerida"></asp:RequiredFieldValidator>
                               
                <br />
                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ControlToValidate="txtCantidad" Display="Dynamic" 
                        ErrorMessage="Debe  ingresar una cantidad valida" 
                        onservervalidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
    
				
		         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                     ControlToValidate="txtFecha" Display="Dynamic" 
                     ErrorMessage="La fecha es requerida"></asp:RequiredFieldValidator>
      
                 <br />   
            </div>
 
 <div id="formularioArticulo">  
      
  
  <h2>Datos del Conteo </h2>
   <table border="0">
   
   
   <tr>
                <td>
                    <label>Cantidad</label> 
                
                </td>
                <td>
                    <asp:TextBox ID="txtCantidad" runat="server" Width="164px"></asp:TextBox>
                    <br />
                </td>
            </tr>
  
     <tr>
                <td>
                    <label>Fecha</label> 
                
                </td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="datepicker" Width="164px"></asp:TextBox>
                    <br />
                </td>
            </tr>          
 <tr>
 
 <td><label>Responsable</label></td>
 <td> <asp:dropdownlist runat="server" Height="25px" Width="164px" 
         ID="lstResponsable"></asp:dropdownlist></td>
 
 </tr>           

            <tr>
                <td>
              <label>Obs</label>
                </td>
                <td>
                    <asp:TextBox ID="txtObs" runat="server" Width="164px" TextMode="MultiLine"></asp:TextBox>
                    <br />

                </td>
            </tr>    

   </table> 

               <div>  
               
                 <table border="0">
                 <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click1" /> </td>
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

