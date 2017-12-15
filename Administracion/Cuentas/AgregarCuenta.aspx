<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarCuenta.aspx.cs" Inherits="Administracion_Cuentas_AgregarCuenta" Title="Gestionar Cuenta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div align="center">

    
        <div>
           <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            <br />
               <asp:CustomValidator ID="CustomValidator1" runat="server" 
                   ControlToValidate="txtUsuario" Display="Dynamic" ErrorMessage="CustomValidator" 
                   onservervalidate="CustomValidator1_ServerValidate">Este usuario ya esta en 
               uso</asp:CustomValidator>
                    <br />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="txtUsuario" Display="Dynamic" 
                   ErrorMessage="RequiredFieldValidator">El usuario es requerido</asp:RequiredFieldValidator>
        </div>
        
  <div id="formularioArticulo">  
     <h2>Agregar Usuarios</h2>
        <table border="0">
                    
          <tr>
           <td><label for="txtUsuario">Usuario</label></td>
           <td> 
               <asp:TextBox ID="txtUsuario" runat="server" Width="250px"></asp:TextBox>
               <br />
                    </td>
          </tr>
          
           <tr>
             <td><label for="txtClave">Clave</label></td>
             <td> <asp:TextBox ID="txtClave" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                 <br />
                    </td>
          </tr>
        
          <tr>
             <td><label for="txtConfirmacion">Confirmacion</label></td>
             <td> <asp:TextBox  ID="txtConfirmacion" runat="server" Width="250px" 
                     TextMode="Password"></asp:TextBox>
                 <br />
                 <asp:CompareValidator ID="CompareValidator1" runat="server" 
                     ControlToCompare="txtClave" ControlToValidate="txtConfirmacion" 
                     Display="Dynamic" ErrorMessage="El campo de clave y confirmacion no concuerdan"></asp:CompareValidator>
                    </td>
          </tr>
                            
           <tr>
             <td><label for="lstResponsable">Responsable</label></td>
             <td> 
                 <asp:DropDownList ID="lstResponsable" runat="server" Height="19px" 
                     Width="250px">
                 </asp:DropDownList>
                    </td>
          </tr>
          
          <tr>
             <td><label for="lstRol">Rol</label></td>
             <td> 
                 <asp:DropDownList ID="lstRol" runat="server" Height="21px" Width="250px">
                 </asp:DropDownList>
                    </td>
          </tr>
       
        </table>

<div id="btnForma">
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  ImageUrl="~/Imagenes/guardar.png" OnClick="btnGuardar_Click"  /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  ImageUrl="~/Imagenes/atras.png" OnClick="btnCancelar_Click"  CausesValidation="false" /> </td></tr>
           </table>
 </div>
   </div>     
        <br />
        
    </div>
</asp:Content>