<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarMantenimiento.aspx.cs" Inherits="Administracion_Mantenimiento_Default" Title="Realizar Solicitud" %>

<%@ Register src="../../CustomControls/BuscarArticulo.ascx" tagname="BuscarArticulo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <uc1:BuscarArticulo ID="BuscarArticulo1" runat="server" />
  
  <div align="center">

      <div id="formularioArticulo" >
       <br />
      <h2>Realizar Solicitud</h2>
         
          <asp:ValidationSummary ID="ValidationSummary1" runat="server"  />
          <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
          <br />
      <asp:RequiredFieldValidator ID="reqFecha" runat="server"  
              ErrorMessage="Debe indicar una fecha de solicitud" Display="None" 
              ControlToValidate="txtFecha"></asp:RequiredFieldValidator>

      <asp:RequiredFieldValidator ID="reqFecha2" runat="server"  
              ErrorMessage="Debe indicar una descripción" Display="None" 
              ControlToValidate="txtDescripcion"></asp:RequiredFieldValidator>         
 
       <asp:RequiredFieldValidator ID="reqControl" runat="server"  
              ErrorMessage="Debe indicar un articulo" Display="None" 
              ControlToValidate="txtControl"></asp:RequiredFieldValidator>      
                          
          <asp:CustomValidator ID="ctsControl" runat="server"  
              ControlToValidate="txtControl" Display="None" 
              ErrorMessage="Debe indicar un control valido" 
              onservervalidate="ctsControl_ServerValidate"></asp:CustomValidator>
              
             
       <table border="0">   
          
          <tr>
             <td><asp:Label ID="Label1" runat="server" Text="Codigo Articulo" AssociatedControlID="txtControl" ></asp:Label></td> 
             <td>

                 <asp:TextBox ID="txtControl"  Width="130"  runat="server"></asp:TextBox>
             
              <a href="#myDivID" id="fancyBoxLink"> <asp:ImageButton ID="btnAbrir" runat="server"   
                ImageUrl="~/Imagenes/buscar.png"  ToolTip="Buscar" Height="20" Width="20"   
                     CausesValidation="false" onclick="btnAbrir_Click"  /> </a> </td>
          </tr>
          
          <tr>
             <td><asp:Label ID="Label2" runat="server" Text="Fecha Solicitud" AssociatedControlID="txtFecha" ></asp:Label></td> 
             <td><asp:TextBox ID="txtFecha"  Width="150"  CssClass="datepickers"   runat="server"></asp:TextBox></td>
          </tr>

          <tr>
             <td><asp:Label ID="Label3" runat="server" Text="Atendido por"  AssociatedControlID="lstResponsable"   ></asp:Label></td> 
             <td><asp:DropDownList ID="lstResponsable" Width="150"  runat="server">  </asp:DropDownList></td>
          </tr>
 
           <tr>
             <td>Solicitado por</td> 
             <td><asp:DropDownList ID="lstUsuario" Width="150"  runat="server">  </asp:DropDownList></td>
          </tr>

           <tr>

             <td><asp:Label ID="Label5" runat="server" Text="Fecha Recepción" AssociatedControlID="txtRecepcion" ></asp:Label></td> 
             <td><asp:TextBox ID="txtRecepcion"  Width="150"  CssClass="datepickers"   runat="server"></asp:TextBox></td>
          </tr> 
                              
          <tr>
             <td ><asp:Label ID="Label4" runat="server" Text="Descripción" AssociatedControlID="txtDescripcion" ></asp:Label></td> 
             <td><asp:TextBox ID="txtDescripcion" runat="server" MaxLength="500" Width="150"  Rows="2" TextMode="MultiLine" ></asp:TextBox></td>
          </tr>
        
        
          <tr>
              
          <td></td>
              <td> <asp:CheckBox ID="chkTraslado" runat="server" Text="Realizar Traslado" /> </td>
          </tr>  
          
          
       </table>             

               <div id="btnForma">

            <br />
            
            
            <table border="0">
            
             <tr> <td> 
                 <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click"/> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" 
                     onclick="btnCancelar_Click"   />
                      </td></tr>
            </table>
           
           
           
           <br />
        </div>
        
         
      </div>
  </div>
</asp:Content>

