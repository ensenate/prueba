<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarArticulo.aspx.cs"
    Inherits="Inventario_AgregarInventario" MasterPageFile="~/MasterPage.master"
    Title="Gestionar Articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <div id="formularioArticulo2">
            <br />
            <h2>
                Articulo
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </h2>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            <div>
                <asp:CustomValidator ID="vldObs" runat="server" ErrorMessage="Si deja inactivo el articulo debe ingresar una observacion"
                    OnServerValidate="vldObs_ServerValidate"></asp:CustomValidator>
            </div>
            <table border="0">
                <tr>
                    <td>
                        <label for="lstCategoria">
                            Categoria</label><b> </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="lstCategoria" runat="server" Height="23px" Width="200px" OnSelectedIndexChanged="lstCategoria_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:ImageButton ID="ImageButtonCN" runat="server" Height="16px" ImageUrl="~/Imagenes/agregarp.png"
                            Width="16px" ToolTip="Agregar" CausesValidation="false" OnClick="ImageButtonCN_Click" />
                        <asp:ImageButton ID="ImageButtonCA" runat="server" Height="16px" ImageUrl="~/Imagenes/refrescar.png"
                            Width="16px" CausesValidation="false" ToolTip="Actualizar" OnClick="ImageButtonCA_Click" />
                    </td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <td>
                                <label for="lstMarca">
                                    Marca</label><b> </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="lstMarca" runat="server" Height="19px" Width="200px" OnSelectedIndexChanged="lstMarca_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:ImageButton ID="ImageButtonMN" runat="server" Height="16px" ImageUrl="~/Imagenes/agregarp.png"
                                    Width="16px" ToolTip="Agregar" CausesValidation="false" OnClick="ImageButtonMN_Click" />
                                <asp:ImageButton ID="ImageButtonMA" runat="server" Height="16px" ImageUrl="~/Imagenes/refrescar.png"
                                    Width="16px" CausesValidation="false" ToolTip="Actualizar" OnClick="ImageButtonMA_Click" />
                            </td>
                            <td>
                                <label for="lstModelo">
                                    Modelo</label><b> </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="lstModelo" runat="server" Height="23px" Width="200px">
                                </asp:DropDownList>
                                <asp:ImageButton ID="ImageButtonMON" runat="server" Height="16px" ImageUrl="~/Imagenes/agregarp.png"
                                    Width="16px" ToolTip="Agregar" CausesValidation="false" OnClick="ImageButtonMON_Click" />
                                <asp:ImageButton ID="ImageButtonMOA" runat="server" Height="16px" ImageUrl="~/Imagenes/refrescar.png"
                                    Width="16px" CausesValidation="false" ToolTip="Actualizar" OnClick="ImageButtonMOA_Click" />
                            </td>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </tr>
                <tr>
                    <td>
                        <label for="txtSerial">
                            Serial
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSerial" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <label for="lstEmpresa">
                            Empresa</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="lstEmpresa" runat="server" Height="23px" Width="200px">
                        </asp:DropDownList>
                        <asp:ImageButton ID="ImageButtonEN" runat="server" Height="16px" ImageUrl="~/Imagenes/agregarp.png"
                            Width="16px" ToolTip="Agregar" CausesValidation="false" OnClick="ImageButtonEN_Click" />
                        <asp:ImageButton ID="ImageButtonEA" runat="server" Height="16px" ImageUrl="~/Imagenes/refrescar.png"
                            Width="16px" CausesValidation="false" ToolTip="Actualizar" OnClick="ImageButtonEA_Click" />
                    </td>
                    <td>
                        <label for="txtExtra1">
                            Extra 1</label><b> </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExtra1" runat="server" Width="182px"></asp:TextBox>
                        <a href="#" runat="server" id="ltrExtra1" class="tooltipExtra" title="">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ayuda.png" /></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="txtExtra2">
                            Extra 2</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExtra2" runat="server" Width="200px"></asp:TextBox>
                        <a href="#" runat="server" id="ltrExtra2" class="tooltipExtra" title="">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/ayuda.png" /></a>
                    </td>
                    <td>
                        <label for="chkActivo">
                            Activo</label><b> </b>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo" runat="server" Text="Activar Articulo" Checked="True" />
                    </td>
                    <td>
                        <label for="txtObs">
                            Motivo</label><b> </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtObs" runat="server" Width="199px" Height="19px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="txtDetalle">
                            Detalle1</label><b> </b>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtDetalle" runat="server" Width="800px" TextMode="MultiLine" Height="18px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="txtDetalle2">
                            Detalle 2</label><b> </b>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtDetalle2" runat="server" Width="800px" TextMode="MultiLine" Height="18px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <label for="txtImagen">
                            Imagen</label><b> </b>
                    </td>
                    <td colspan="5">
              
                       <div class="cnt_upload">
                          <a href="#myDivIDArticulo" id="fancyBoxLinkArticulo">   <asp:ImageButton ID="ImageButton5" runat="server"  CausesValidation="false" ToolTip="Ver Imagen" ImageUrl="~/Imagenes/buscar.png" Style="width: 15px;height:15px;" /> </a>
                          <asp:TextBox ID="upload_value" runat="server" ReadOnly="true" Text="sinimagen.png"></asp:TextBox>
                       <div class="upload">
                          <asp:FileUpload ID="txtImagen" runat="server"  onchange="document.getElementById('ctl00_ContentPlaceHolder1_upload_value').value=this.value"  />
                       </div>  
              
                 </div>
                 
                    </td>
                </tr>      
                <tr>
                   <td> </td>
                   <td colspan="5"><span class="texto-amarillo" >Formatos Validos .bmp | .gif  |  .png   |  .jpeg  | .jpg</span></td>
                </tr>         
            </table>
            <!-- nuevo -->
            <div id="divPc" runat="server" visible="false">
                <h2>
                    Datos del PC</h2>
                <table border="0">
                    <tr>
                        <td>
                            <label for="txtGrupo">
                                Grupo</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGrupo" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtNombreMaquina">
                                Nombre Equipo</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreMaquina" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtSistema">
                                S.O.</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSistema" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtUusario">
                                Usuario</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUusario" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtIp">
                                Ips</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIp" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtMac">
                                Mac</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMac" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtNombreRed">
                                Nombre Red</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreRed" runat="server" Width="199px" Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtFechaCompra">
                                Compra</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaCompra" runat="server" CssClass="datepicker" Width="199px"
                                Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtFechaGarantia">
                                Garantia</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaGarantia" CssClass="datepickera" runat="server" Width="199px"
                                Height="23px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtFechaFormateo">
                                Formateo</label><b> </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaFormateo" CssClass="datepicker" runat="server" Width="199px"
                                Height="23px"></asp:TextBox>
                        </td>
                        <td>
                            <label for="txtPermisos">
                                Permisos</label><b> </b>
                            
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPermisos" runat="server" Width="480px" Height="20px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtProgramas">
                                Programas</label><b> </b>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtProgramas" runat="server" Width="800px" Height="20px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <h3>
                    Keywords</h3>
                <table border="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/Imagenes/agregarp.png"
                                Width="16px" ToolTip="Agregar" CausesValidation="false" OnClick="ImageButtonKN_Click" />
                            <asp:ImageButton ID="ImageButton4" runat="server" Height="16px" ImageUrl="~/Imagenes/refrescar.png"
                                Width="16px" CausesValidation="false" ToolTip="Actualizar" OnClick="ImageButtonKA_Click" />
                            <asp:ListBox ID="lsbTodo" runat="server" Height="136px" Width="163px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/derecha.png"
                                OnClick="ImageButton2_Click" />
                            <br />
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/izquierda.png"
                                OnClick="ImageButton3_Click" />
                        </td>
                        <td>
                            <asp:ListBox ID="lsbArticulo" runat="server" Height="131px" Width="164px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="btnForma" align="center">
                <br />
                <table border="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/Imagenes/guardar.png"
                                OnClick="btnGuardar_ClickG" Style="width: 50px" />
                        </td>
                        <td>
                            <div>
                            </div>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Imagenes/atras.png"
                                OnClick="btnCancelar_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </div>
    </div>
    <br />
    <br />
    <div id="observacion">
    </div>
    
    <div style="display:none">
       <div id="myDivIDArticulo">  
          <asp:Image ID="imgArticulo" runat="server" ImageUrl="~/Recursos/Articulos/sinimagen.png" AlternateText="No se pudo cargar la imagen" />
       </div>
    </div>
    
</asp:Content>

