<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarModelo.aspx.cs" Inherits="Administracion_Dominio_AgregarModelo" Title="Agregar Modelo" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center">
    

<div>         <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtNombre" ErrorMessage="Es Requerido un Nombre" 
        Display="Dynamic"></asp:RequiredFieldValidator>
    <br />
        </div>
        
<div id="formularioArticulo">
<h2>Agregar Modelo</h2>

<table>
<tr>
      <td> <label for="txtNombre">Nombre</label></td>
       <td> <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
</tr>
<tr>
      <td> <label for="lstMarca">Marca</label></td>
       <td> 
           <asp:DropDownList ID="lstMarca" runat="server" Height="25px" Width="150px">
           </asp:DropDownList>
      </td>
</tr>
</table>      

<br />     
<div id="btnForma">
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

