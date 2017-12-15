<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarUbicacion.aspx.cs" Inherits="Matenimiento_Ubicacion_AgregarUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="center" >
    
            
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="El Nombre es Obligatorio" ControlToValidate="txtNombre" 
                   Display="Dynamic"></asp:RequiredFieldValidator>
 
  <div id="formularioArticulo">
  <h2>Agregar Ubicacion</h2>
        <table border="0">    
        <tr>
           <td ><label for="txtNombre">Nombre</label></td>
           <td>   <asp:TextBox ID="txtNombre" runat="server" Width="159px"></asp:TextBox>
               <br />
           </td>
       </tr>
            <tr>
              <td><label for="txtDescripcion">Descricion </label> </td>
              <td>   <asp:TextBox ID="txtDescripcion" runat="server" Width="158px"></asp:TextBox>
                  <br />
              </td>
           </tr>
           <tr>
             <td> <label for="lstEmpresa">Empresa </label> </td>
             <td> 
                 <asp:DropDownList ID="lstEmpresa" runat="server" Height="25px" Width="161px">
                 </asp:DropDownList>
                        </td>
           </tr>           
         </table>  

              
         <div id="btnForma" align= "center">
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  ImageUrl="~/Imagenes/guardar.png" OnClick="btnGuardar_Click"  /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  ImageUrl="~/Imagenes/atras.png" OnClick="btnCancelar_Click"  CausesValidation="false" /> </td></tr>
           </table>
        </div>  
</div>
        <br />
         <br />
    </div>                  
</asp:Content>
