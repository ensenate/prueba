<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarExistencia.aspx.cs" Inherits="Inventario_Desgaste_Existencia_AgregarExistencia" Title="Consumo del Articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   <div align="center">
   <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
   
  
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
                <br />
            </div>
 
 <div id="formularioArticulo">  
  <h2>Agregar Consumo </h2>
   <table border="0">
   
   
   <tr>
   <td><label>Tipo</label></td>
   <td>
       <asp:DropDownList ID="lstTipo" runat="server" Height="25px" Width="164px">
           <asp:ListItem Selected="True" Value="E">Entrada</asp:ListItem>
           <asp:ListItem Value="S">Salida</asp:ListItem>
       </asp:DropDownList>
                    </td>
   </tr>
   
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
 
 <td><label>Responsable</label></td>
 <td> <asp:dropdownlist runat="server" Height="25px" Width="162px" 
         ID="lstResponsable"></asp:dropdownlist></td>
 
 </tr>           
 
                  <tr>
           
                <td>
                    
                    <label for="lstEmpresa">
                        Empresa</label>
                
                </td>
                <td>

                <asp:DropDownList  ID="lstEmpresa" runat="server"    Height="22px" Width="163px" 
                 AutoPostBack="True" onselectedindexchanged="lstEmpresa_SelectedIndexChanged1" >    </asp:DropDownList>

                </td>
            </tr>
    
            <tr>
                <td>
                    
                    <label for="lstUbicacion">
                        Ubicacion</label>
               
                </td>
                <td>
                <asp:DropDownList ID="lstUbicacion" runat="server" Height="22px" Width="163px">
                </asp:DropDownList>
                </td>
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
       </div>       
                               
   </div>   

</asp:Content>

