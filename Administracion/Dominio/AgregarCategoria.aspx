<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarCategoria.aspx.cs" Inherits="Administracion_Categoria_AgregarCategoria" Title="Gestionar Categorias" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center" >
      
       
        <div>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                      ControlToValidate="txtPrefijo" Display="Dynamic" 
                      ErrorMessage="El Prefijo es Obligatorio"></asp:RequiredFieldValidator>
              <br />
           <asp:CustomValidator ID="vldPrefijo" runat="server" 
                      ErrorMessage="El Prefijo de la Categoria ya Esta en Uso" 
                      onservervalidate="vldPrefijo_ServerValidate"></asp:CustomValidator>

           <br />
           <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="El Nombre es Obligatorio" ControlToValidate="txtNombre" 
                   Display="Dynamic"></asp:RequiredFieldValidator>
               <br />
               <asp:CustomValidator ID="vldNombre" runat="server" 
                   ControlToValidate="txtNombre" 
                   ErrorMessage="El Nombre de la Categoria ya Esta en Uso" 
                   onservervalidate="CustomValidator1_ServerValidate" Display="Dynamic"></asp:CustomValidator>
        </div>
        
 <div id="formularioCategoria">       
  <h2>Agregar Categoria</h2>
        <table border="0">    
        <tr>
           <td><label for="txtNombre">Nombre</label></td>
           <td>   
               <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
               <br />
           </td>
       </tr>
            <tr>
              <td><label for="txtPrefijo">Prefijo </label> </td>
              <td>   

                 
                  <asp:TextBox ID="txtPrefijo" runat="server" Width="200px"></asp:TextBox>
                  <br />
              </td>
           </tr>
            <tr>
              <td><label for="txtEjemplo1">Ejemplo 1</label> </td>
              <td>   

                 
                  <asp:TextBox ID="txtEjemplo1" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                  <br />
              </td>
           </tr>
              <tr>
              <td><label for="txtEjemplo2">Ejemplo 2</label> </td>
              <td>   

                 
                  <asp:TextBox ID="txtEjemplo2" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                  <br />
              </td>
                  
           </tr>         
 
            <tr>
              <td><label for="txtImagen">Imagen</label> </td>
              <td>   
                 <div class="cnt_upload">
                    <a href="#myDivIDCategoria" id="fancyBoxLinkCategoria">   <asp:ImageButton ID="ImageButton5" runat="server"  CausesValidation="false" ToolTip="Ver Imagen" ImageUrl="~/Imagenes/buscar.png" Style="width: 15px;height:15px;" /> </a>
                    <asp:TextBox ID="upload_value" runat="server" ReadOnly="true" Text="sinimagen.png"></asp:TextBox>
                    <div class="upload">
                      <asp:FileUpload ID="txtImagen" runat="server"  onchange="document.getElementById('ctl00_ContentPlaceHolder1_upload_value').value=this.value"  />
                    </div>  
                 </div>
                 
                  <br />
              </td>
           </tr>   
 
            <tr>
              <td><label for="ckAsociar">Asociar</label> </td>
              <td>   
                  <asp:CheckBox ID="ckAsociar" runat="server" />
                  <br />
              </td>
           </tr>   
            <tr>
              <td><label for="chkPc">PC</label> </td>
              <td>   
                  <asp:CheckBox ID="chkPc" runat="server" />
                  <br />
              </td>
           </tr>         
         </table> 
                  <div id="btnForma" align= "center">
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  ImageUrl="~/Imagenes/guardar.png" OnClick="btnGuardar_Click"  /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  ImageUrl="~/Imagenes/atras.png" OnClick="btnCancelar_Click"  CausesValidation="false" /> </td></tr>
           </table>
           <br />
        </div>
         
        </div> 
   
    </div>  

    <div style="display:none">
       <div id="myDivIDCategoria">  
          <asp:Image ID="imgCategoria" runat="server" ImageUrl="~/Recursos/Categorias/sinimagen.png" AlternateText="No se pudo cargar la imagen" />
       </div>
    </div>
    
</asp:Content>