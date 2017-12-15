<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarPersonal.aspx.cs" Inherits="Matenimiento_Personal_AgregarPersonal" Title= "Gestionar Personal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div align="center" >
            
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
   
            <br />
   
   <div id="formularioArticulo">
   <h2>Agregar Personal</h2>
        <table border="0">    
        <tr>
           <td><label for="txtRif">Rif </label></td>
           <td>   <asp:TextBox ID="txtRif" runat="server" Width="160px"></asp:TextBox>
               <br />
           </td>
       </tr>
            <tr>
              <td><label for="txtNombre">Nombre </label> </td>
              <td>   <asp:TextBox ID="txtNombre" runat="server" Width="160px"></asp:TextBox>
                  <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombre" 
                      Display="Dynamic"></asp:RequiredFieldValidator>
              </td>
           </tr>
           <tr>
             <td> <label for="txtTelefono">Telefono</label><b> </b> </td>
             <td> <asp:TextBox ID="txtTelefono" runat="server" Width="160px"></asp:TextBox>  </td>
           </tr>
            <tr>
             <td> <label for="txtCorreo">Correo</label><b> </b> </td>
             <td> <asp:TextBox ID="txtCorreo" runat="server" Width="160px"></asp:TextBox>  </td>
           </tr>
                      
           <tr>
             <td> <label for="txtTipo">Interno</label><b> </b> </td>
             <td> 
                 <asp:CheckBox ID="txtTipo" runat="server" Checked="true" 
                     oncheckedchanged="txtTipo_CheckedChanged1"  /> </td>
           </tr>               
            <tr>
             <td> <label for="lstEmpresa">Empresa</label><b> </b> </td>
             <td> <asp:DropDownList ID="lstEmpresa" runat="server" Width="160px"></asp:DropDownList>  </td>
           </tr>
        
         </table>  

        <br />
              
         <div id="btnForma" align= "center">
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  ImageUrl="~/Imagenes/guardar.png" OnClick="btnGuardar_Click"  /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  ImageUrl="~/Imagenes/atras.png" OnClick="btnCancelar_Click"  CausesValidation="false" /> </td></tr>
           </table>
         </div>  
         <br />
        </div> 
    </div>                 
    
</asp:Content>