<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarEmpresa.aspx.cs" Inherits="Administracion_Empresa_AgregarEmpresa" Title="Gestionar Empresa" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="center" >
         
            <div align="center">
              
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ErrorMessage="Ya hay una Empresa con este Nombre" 
                                ControlToValidate="txtNombre" 
                                onservervalidate="CustomValidator1_ServerValidate1" Display="Dynamic"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNombre" Display="Dynamic" 
                                ErrorMessage="El Nombre es Obligatorio"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtRif" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">El Rif es Obligatorio</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                ControlToValidate="txtRif" Display="Dynamic" 
                                ErrorMessage="Ya hay una Empresa con este Rif" 
                                onservervalidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                <br />
            <div id="formularioArticulo">
              <h2> Agregar Empresa</h2>
                <table border="0">
                    <tr>
                        <td> <label for="txtNombre"> Nombre</label></td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="159px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <label for="txtRif">
                            Rif
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRif" runat="server" Width="158px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                       <td> <label for="txtTelefono">Telefono</label></td>
                       <td><asp:TextBox ID="txtTelefono" runat="server" Width="159px"></asp:TextBox></td>
                    </tr>

                    <tr>
                       <td> <label for="txtDireccion">Direccion</label></td>
                       <td><asp:TextBox ID="txtDireccion" runat="server" Width="153px"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                       <td> <label for="txtDescripcion">Descripcion</label></td>
                       <td><asp:TextBox ID="txtDescripcion" runat="server" Width="155px"></asp:TextBox></td>
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
                

                    
                    
            </div>

        <br />
              
    </div>        

</asp:Content>