<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReparacionExterna.aspx.cs" Inherits="Administracion_Mantenimiento_ReparacionExterna" Title="Soporte Externo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="center">

    <h2>Soporte Externo</h2>
    <div class="formumario-reparacion-externa">
       
       <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Italic="False" 
            ForeColor="Red"></asp:Label>
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
       
        <table border="0">

            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Responsable" AssociatedControlID="lstResponsable"  ></asp:Label></td>
                <td><asp:DropDownList  ID="lstResponsable" runat="server" Width="200" ></asp:DropDownList ></td>
            </tr>
            
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Envio" AssociatedControlID="txtFechaEnvio"   ></asp:Label></td>
                <td><asp:TextBox ID="txtFechaEnvio" runat="server" Width="200"  CssClass="datepickers" ></asp:TextBox></td>
            </tr>
 
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Descripción" AssociatedControlID="txtDescripcion" ></asp:Label></td>
                <td><asp:TextBox ID="txtDescripcion" runat="server"  MaxLength="500" TextMode="MultiLine"  Rows="2"  Width="200" ></asp:TextBox></td>
            </tr>  
            
             <tr><td></td>
                <td ><asp:CheckBox ID="chkTraslado" runat="server" Text="Realizar Traslado"  /></td>
            </tr>  
         
        </table> 
        
         <asp:RequiredFieldValidator ID="reqFecha" ControlToValidate="txtFechaEnvio" runat="server" 
            ErrorMessage="La fecha es requerida"></asp:RequiredFieldValidator>

         <asp:RequiredFieldValidator ID="reqDescripcion" runat="server" ControlToValidate="txtDescripcion" 
            ErrorMessage="La descripcion es requerida"></asp:RequiredFieldValidator>
                    
         <br />
            
        <table border="0">
            
             <tr> <td> 
                 <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" 
                     onclick="btnCancelar_Click"    />
               </td>
             </tr>
         </table>
           
           
    </div>

</div>


</asp:Content>

