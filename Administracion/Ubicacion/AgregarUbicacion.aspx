<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarUbicacion.aspx.cs" Inherits="Matenimiento_Ubicacion_AgregarUbicacion" Title="Gestionar Ubicaciones" %>

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
           <td ><label for="txtNombre">Descripcion</label></td>
           <td>   <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
               <br />
           </td>
       </tr>
            <tr>
              <td><label for="txtDescripcion"> Detalle</label> </td>
              <td>   <asp:TextBox ID="txtDescripcion" runat="server" Width="200px"></asp:TextBox>
                  <br />
              </td>
           </tr>
           <tr>
             <td> <label for="lstEmpresa">Empresa </label> </td>
             <td> 
                 <asp:DropDownList ID="lstEmpresa" runat="server" Height="25px" Width="200px">
                 </asp:DropDownList>
                        </td>
           </tr>
           
           
           <tr>
              <td><label for="btnImagen">Imagen</label> </td>
              <td>
                 
                 <div class="cnt_upload">
                    <a href="#myDivIDUbicacion" id="fancyBoxLinkUbicacion">   <asp:ImageButton ID="ImageButton5" runat="server"  CausesValidation="false" ToolTip="Ver Imagen" ImageUrl="~/Imagenes/buscar.png" Style="width: 15px;height:15px;" /> </a>
                    <asp:TextBox ID="upload_value" runat="server" ReadOnly="true" Text="sinimagen.png"></asp:TextBox>
                    <div class="upload">
                      <asp:FileUpload ID="txtImagen" runat="server"  onchange="document.getElementById('ctl00_ContentPlaceHolder1_upload_value').value=this.value"  />
                    </div>  
                 </div>
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
    
        <div style="display:none">
       <div id="myDivIDUbicacion">  
          <asp:Image ID="imgUbicacion" runat="server" ImageUrl="~/Recursos/ubicaciones/sinimagen.png" AlternateText="No se pudo cargar la imagen" />
       </div>
    </div>
          
</asp:Content>
