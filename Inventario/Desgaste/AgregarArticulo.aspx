<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarArticulo.aspx.cs" Inherits="Desgaste_AgregarProducto" Title="Gestionar Consumos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
    <div align="center"> 

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                        
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtDesc" Display="Dynamic" 
                        ErrorMessage="La Descripcion es Obligatoria"></asp:RequiredFieldValidator>
            <br />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="txtDesc" Display="Dynamic" 
                        ErrorMessage="Ya esta descripcion esta siendo usada por otro articulo" 
                        onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>

            
     <div id="formularioArticulo">  
        
        <br />
        <h2>Articulo Consumible</h2>
        
        <table border="0">

            
                        <tr>
                <td >
                    <label for="txtDesc">
                        Descripcion</label>
                </td>
                <td>

                    <asp:TextBox ID="txtDesc" runat="server" Width="157px"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
            <tr>
                <td>
                    <label for="lstCategoria">
                        Categoria</label><b> </b>
                </td>
                <td>
                    <asp:DropDownList ID="lstCategoria" runat="server" Height="23px" Width="160px">
                    </asp:DropDownList>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/agregarp.png" Width="16px"  ToolTip="Agregar"
                      CausesValidation="false"  OnClick="ImageButton1_Click"/>
                   
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/refrescar.png" Width="16px" 
                         CausesValidation="false" ToolTip="Actualizar"  OnClick="ImageButton10_Click"  />
                </td>
            </tr> 
            
            <tr>
                <td>
                    <label for="lstMarca">
                        Marca</label><b> </b>
                </td>
                <td>
                    <asp:DropDownList ID="lstMarca" runat="server" Height="19px" Width="160px" OnSelectedIndexChanged="lstMarca_SelectedIndexChanged" AutoPostBack="True" >
                    </asp:DropDownList>
                        <asp:ImageButton ID="ImageButton5" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/agregarp.png" Width="16px"    CausesValidation="false" OnClick="ImageButton5_Click" />
                        
                        <asp:ImageButton ID="ImageButton6" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/refrescar.png" Width="16px" 
                         CausesValidation="false" ToolTip="Actualizar"  OnClick="ImageButton6_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="lstModelo">Modelo</label></td>
                <td>
                    <asp:DropDownList ID="lstModelo" runat="server" Height="23px" Width="160px">
                    </asp:DropDownList>
                    
                        <asp:ImageButton ID="ImageButton4" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/agregarp.png" Width="16px" CausesValidation="false"  OnClick="ImageButton4_Click"   />
                
                         <asp:ImageButton ID="ImageButton7" runat="server" Height="16px" 
                         ImageUrl="~/Imagenes/refrescar.png" Width="16px" 
                         CausesValidation="false" ToolTip="Actualizar"  OnClick="ImageButton7_Click"  />
                
                </td>
            </tr>
  
    
        </ContentTemplate>
        </asp:UpdatePanel> 
             <tr>
                <td>
                    <label for="lstEmpresa">
                        Empresa</label>
                </td>
                <td>

                <asp:DropDownList  ID="lstEmpresa" runat="server"    Height="22px" Width="160px" 
                 AutoPostBack="True">    </asp:DropDownList>
                 
                   <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/agregarp.png" Width="16px" 
                        CausesValidation="false" OnClick="ImageButton2_Click" />
                 
                   <asp:ImageButton ID="ImageButton9" runat="server" Height="16px" 
                         ImageUrl="~/Imagenes/refrescar.png" Width="16px" 
                         CausesValidation="false" ToolTip="Actualizar"  OnClick="ImageButton9_Click"  />
                                

                </td>
            </tr>
              
              <tr>
                <td>
                    <label for="lstUnidad">
                        Unidad</label><b> </b>
                </td>
                <td>
                    <asp:DropDownList ID="lstUnidad" runat="server" Height="23px" Width="160px">
                    </asp:DropDownList>
                        <asp:ImageButton ID="ImageButton3" runat="server" Height="16px" 
                        ImageUrl="~/Imagenes/agregarp.png" Width="16px" 
                        CausesValidation="false" OnClick="ImageButton3_Click"  />
                        
                     <asp:ImageButton ID="ImageButton8" runat="server" Height="16px" 
                         ImageUrl="~/Imagenes/refrescar.png" Width="16px" 
                         CausesValidation="false" ToolTip="Actualizar"  OnClick="ImageButton8_Click"  />
                </td>
            </tr>              
                        
        </table>
         
        <div id="btnForma">
            <br />
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click1" /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" 
                     onclick="btnCancelar_Click" /> </td></tr>
           </table>
        </div>
        <br />
 </div>
         </div>
</asp:Content>


