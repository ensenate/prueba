<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Recepcion.aspx.cs" Inherits="Administracion_Mantenimiento_Recepcion" Title="Realizar Recepcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  
  <div align="center">
  
  <h2>Recepción de Articulo</h2>
    
    <div class="informacion-recepcion"  >
        
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
              
              <td class="style3"><span>Usuario</span></td>
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
    
    <div class="formulario-recepcion">
        <asp:RequiredFieldValidator ID="ReqFecha" runat="server" ErrorMessage="La fecha es requerida"  ControlToValidate="txtFecha" Display="Dynamic" ></asp:RequiredFieldValidator>
        <asp:Label ID="lblMensaje" runat="server" Font-Italic="True" ForeColor="Red" ></asp:Label>
    
        <br />
    
        <table>
        <tr>
           <td><asp:Label ID="Label6" runat="server" Text="Fecha Recepcion" AssociatedControlID="txtFecha"></asp:Label></td>
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
    
    
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">


    <link rel="icon" href="<%=ResolveUrl("~/favicon.ico")%>"  type="image/ico"/>

    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/ui-lightness/jquery-ui-1.8.23.custom.css")%>"  /> 
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/css/general.css")%>"  />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    
    <script src="<%=ResolveUrl("~/Util/js/jquery-1.8.0.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery-ui-1.8.23.custom.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/jquery.ui.datepicker-es.js")%>" language="javascript" type="text/javascript"></script> 
	<script src="<%=ResolveUrl("~/Util/js/jquery.qtip-1.0.0-rc3.min.js")%>" language="javascript" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Util/js/Script.js")%>" language="javascript" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.8.3.js" language="javascript" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js" language="javascript" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/Util/FancyBox/jquery.fancybox.js")%>"  language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=ResolveUrl("~/Util/FancyBox/jquery.fancybox.css")%>" type="text/css" media="screen" />

    
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


