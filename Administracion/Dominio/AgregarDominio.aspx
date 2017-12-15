<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarDominio.aspx.cs" Inherits="Matenimiento_Dominio_AgregarDominio" Title="Gestionar Dominios" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center">
    

<div>         <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtNombre" ErrorMessage="Es Requerido un Nombre" 
        Display="Dynamic"></asp:RequiredFieldValidator>
    <br />
        </div>
        
<div id="formularioArticulo">
<h2><asp:Literal ID="ltrTitulo" runat="server"></asp:Literal></h2>
        <label for="txtNombre">Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
      

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