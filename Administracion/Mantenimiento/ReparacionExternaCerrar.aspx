<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReparacionExternaCerrar.aspx.cs" Inherits="Administracion_Mantenimiento_ReparacionExternaCerrar" Title="Cerrar Soporte Externo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">

<h2>Soporte Externo</h2>
<asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

<div class="formulario-cerrar-soporte-externo">

    <table border="0">
 
       <tr>
          <td><asp:Label ID="Label1" runat="server" Text="Fecha Recepción" AssociatedControlID="txtFecha" ></asp:Label></td>
          <td><asp:TextBox ID="txtFecha" runat="server" CssClass="datepickers" Width="100" ></asp:TextBox></td>
       </tr>


       <tr>
          <td><asp:Label ID="Label3" runat="server" Text="Costo" AssociatedControlID="txtCosto"    ></asp:Label></td>
          <td><asp:TextBox ID="txtCosto" runat="server" Width="100"  ></asp:TextBox></td>
       </tr>
       
       <tr>
          <td><asp:Label ID="Label2" runat="server" Text="Diagnostico" AssociatedControlID="txtDiagnostico"    ></asp:Label></td>
          <td><asp:TextBox ID="txtDiagnostico" runat="server" MaxLength="500" TextMode="MultiLine" Rows="3" Width="200" ToolTip="No ingrese punto para indicar los miles"  ></asp:TextBox></td>
       </tr>

    </table>
    
    <asp:RequiredFieldValidator ID="reqFecha" runat="server"  ControlToValidate="txtFecha"
        ErrorMessage="La fecha es requerida" Display="None"></asp:RequiredFieldValidator>
        
    <asp:RequiredFieldValidator ID="reqCosto" runat="server"  ControlToValidate="txtCosto"
        ErrorMessage="El costo es requerido"></asp:RequiredFieldValidator>
        
    <asp:RequiredFieldValidator ID="reqDiagnostico" runat="server"  ControlToValidate="txtDiagnostico"
        ErrorMessage="Debe indicar un diagnostico" Display="None" ></asp:RequiredFieldValidator>
       
    
    <br />
    
           <asp:ImageButton ID="btnGuardar" runat="server"    
            ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> 
        <asp:ImageButton ID="btnCancelar" runat="server"    
            ImageUrl="~/Imagenes/atras.png" CausesValidation="false" 
        onclick="btnCancelar_Click"   />   
    
</div>

</div>


</asp:Content>

