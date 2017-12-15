<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Enviar.aspx.cs" Inherits="Administracion_Mantenimiento_Enviar" Title="Envio de Articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  
  <div align="center">
  
  <h2>Envio de Articulo</h2>
    
    <div class="informacion-envio"  >
        
        <table border="0" style="width: 714px">
            <tr>
              <td class="style1"><span>Control</span></td>
              <td class="style2"><asp:Literal ID="ltrControl" runat="server"></asp:Literal></td>
              
              <td class="style3"><span>Categoria</span></td>
              <td style="text-align: left"><asp:Literal ID="ltrCategoria" runat="server"></asp:Literal></td>

            </tr>

            <tr>
              <td class="style1"><span>Solicitud</span></td>
              <td class="style2"><asp:Literal ID="ltrFecha" runat="server"></asp:Literal></td>
              
              <td class="style3"><span>Solicitado por</span></td>
              <td style="text-align: left"><asp:Literal ID="ltrUsuario" runat="server"></asp:Literal></td>
            </tr>
            
            <tr>
              <td class="style1">Descripción</td>
              <td  colspan="3"><asp:Literal ID="ltrDescripcion" runat="server"></asp:Literal></td>
            </tr>
            
        </table>
    
    </div>
    
    <br />
    <br />
    
    <div class="formulario-envio">
        <asp:RequiredFieldValidator ID="ReqFecha" runat="server" ErrorMessage="La fecha es requerida"  ControlToValidate="txtFecha" Display="Dynamic" ></asp:RequiredFieldValidator>
        <asp:Label ID="lblMensaje" runat="server" Font-Italic="True" ForeColor="Red" ></asp:Label>
    
        <br />
    
        <table>
        <tr>
           <td><asp:Label ID="Label6" runat="server" Text="Fecha Envio" AssociatedControlID="txtFecha"></asp:Label></td>
           <td><asp:TextBox ID="txtFecha" Width="80" runat="server" CssClass="datepickers" ></asp:TextBox></td>
           <td><asp:CheckBox ID="chkTraslado" runat="server" Text="Realizar Traslado" /> </td>  
        </tr>
        </table>
        
        <br />
        
        <asp:ImageButton ID="btnGuardar" runat="server"    
            ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> 
        <asp:ImageButton ID="btnCancelar" runat="server"    
            ImageUrl="~/Imagenes/atras.png" onclick="btnCancelar_Click" CausesValidation="false"   />        
    </div>
  </div>  
  
     <style type="text/css">
        .style1
        {
            text-align: left;
            width: 75px;
        }
        .style2
        {
            width: 180px;
        }
        .style3
        {
            text-align: left;
            width: 76px;
        }
    </style>

</asp:Content>

