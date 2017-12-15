<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Diagnostico.aspx.cs" Inherits="Administracion_Mantenimiento_Diagnostico" Title="Realizar Diagnostico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">
  
  <div>
      <h2>Realizar Diagnostico</h2>
      
      <div class="formulario-diagnostico" >
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
         <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
         <table border="0">

            <tr>
                <td><asp:Label ID="label4" runat="server" Text="Fecha" 
                        AssociatedControlID="txtFecha"></asp:Label></td>
                <td><asp:TextBox ID="txtFecha"  Width="80" runat="server"  CssClass="datepickers"></asp:TextBox></td>

                <td><asp:Label ID="label5" runat="server" Text="Revisado Por" AssociatedControlID="txtRevision"></asp:Label></td>
                <td><asp:TextBox ID="txtRevision" Width="100" MaxLength="500"  runat="server"></asp:TextBox></td>             
            </tr>
 
             <tr>
                <td><asp:Label ID="label6" runat="server" Text="Diagnostico" AssociatedControlID="txtDiagnostico"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtDiagnostico" Width="300" Rows="4"  runat="server" MaxLength="500" TextMode="MultiLine" ></asp:TextBox></td>             
            </tr>
                                   
         </table>
 

            <asp:RequiredFieldValidator ID="reqFecha" runat="server" 
              ErrorMessage="La fecha es requerida" Display="None" ControlToValidate="txtFecha" ></asp:RequiredFieldValidator >
 

            <asp:RequiredFieldValidator ID="reqRevision" runat="server" 
              ErrorMessage="El revisado por es requerido" Display="None" ControlToValidate="txtRevision" ></asp:RequiredFieldValidator >
              
            <asp:RequiredFieldValidator ID="reqDiagnostico" runat="server" 
              ErrorMessage="El diagnostico es requerido" Display="None" ControlToValidate="txtDiagnostico" ></asp:RequiredFieldValidator >
            <br />

            <table border="0">
            
             <tr> <td> 
                 <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> </td>
             <td></td>
             <td> 
                 <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" onclick="btnCancelar_Click" 
                       />
                      </td></tr>
            </table>

           <br />
           
                </div>

        
</div>

</asp:Content>

