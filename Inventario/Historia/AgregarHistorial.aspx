<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarHistorial.aspx.cs" Inherits="Inventario_Historia_AgregarHistoria" Title="Agregar Movimiento"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
  
    <div align="center">
<div id="formularioArticulo">
<br />
        <h2>
            Agregar Movimiento
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </h2>

      <div><asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
      
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="txtFecha" ErrorMessage="Fecha Invalida" 
                        onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtFecha" Display="Dynamic" 
                        ErrorMessage="La Fecha es Obligatoria"></asp:RequiredFieldValidator>
                    </div> 

        <table border="0">
            <tr>
                <td>
                 <label for="txtFecha">Fecha</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFecha"  CssClass="datepicker" runat="server" Width="163px"></asp:TextBox>
                    <br />
                </td>
            </tr>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <tr>
                <td>
                    <label for="lstEmpresa">
                        Empresa</label>
                </td>
                <td>

                <asp:DropDownList  ID="lstEmpresa" runat="server"    Height="22px" Width="163px" 
                onselectedindexchanged="lstEmpresa_SelectedIndexChanged" AutoPostBack="True">    </asp:DropDownList>

                </td>
            </tr>
    
            <tr>
                <td>
                    <label for="lstUbicacion">
                        Ubicacion</label>
                </td>
                <td>
                <asp:DropDownList ID="lstUbicacion" runat="server" Height="22px" Width="163px">
                </asp:DropDownList>
                </td>
            </tr>
       </ContentTemplate>
        </asp:UpdatePanel>
            <tr>
                <td>
                    <label for="lstResponsable">
                        Responsable</label>
                </td>
                <td>
                    <asp:DropDownList ID="lstResponsable" runat="server" Height="23px" Width="161px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="lstAutorizado">
                        Autorizado</label>
                </td>
                <td>
                    <asp:DropDownList ID="lstAutorizado" runat="server" Height="23px" Width="161px">
                    </asp:DropDownList>
                </td>
            </tr>                        
            <tr>
                <td>
                    <label for="lstEstado">
                        Estado</label>
                </td>
                <td>
                    <asp:DropDownList ID="lstEstado" runat="server" Height="23px" Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtDesc">
                        Obs</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" Width="157px"></asp:TextBox>
                </td>
            </tr>
            
        </table>
               <div id="btnForma">
            <br />
            <table border="0">
             <tr> <td> <asp:ImageButton ID="btnGuardar" runat="server"  
                     ImageUrl="~/Imagenes/guardar.png" onclick="btnGuardar_Click" /> </td>
             <td></td>
             <td> <asp:ImageButton ID="btnCancelar" runat="server"  
                     ImageUrl="~/Imagenes/atras.png"  CausesValidation="false" 
                     onclick="btnCancelar_Click" /> </td></tr>
           </table>
           <br />
        </div>
       

      
</div> 
    </div>
    
    
 <br />
 <br />
 
  
 
<div id="tblAsociados" align="center" >

 <h2>Articulos Asociados</h2>
    
   <div align="center">
        <asp:ImageButton ID="btnMarcalos" runat="server"  
            ImageUrl="~/Imagenes/guardar.png" Height="20" onclick="ImageButton2_Click"  ToolTip="Marcar Todos"   />
        <asp:ImageButton ID="btnDesmarcalos" runat="server"  
            ImageUrl="~/Imagenes/atras.png" Height="20" onclick="ImageButton1_Click"  ToolTip="Desmarcar Todos" />
   </div>
 
 <asp:GridView ID="grdArticulo" runat="server"  BackColor ="#507CD1"   DataKeyNames="id" 
            CellSpacing="2"  AutoGenerateColumns="False"  EmptyDataText="No se encontro ningun articulo asociado" 
      AllowPaging="True"
      onrowcommand="grdArticulo_RowCommand" 
      OnPageIndexChanging="GridView1_PageIndexChanging"
        CellPadding="4" ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="grdArticulo_SelectedIndexChanged" 
            onrowdatabound="grdArticulo_RowDataBound">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                           
             
                          
                <asp:TemplateField >
                <ItemTemplate>
                    <asp:CheckBox ID="chkMarcado" runat="server" Checked="true" />
                </ItemTemplate>
                </asp:TemplateField>
                           
              

              <asp:BoundField DataField="control" HeaderText="Control" 
                SortExpression="control" ReadOnly="true" /> 
                                                                              
            <asp:BoundField DataField="empresa" HeaderText="Empresa" 
                SortExpression="empresa" ReadOnly="true" /> 
                
            <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion" 
                SortExpression="ubicacion"  ReadOnly="true" /> 

                                                              
            <asp:BoundField DataField="categoria" HeaderText="Categoria" 
                SortExpression="categoria" ReadOnly="true" /> 
                
            <asp:BoundField DataField="marca" HeaderText="Marca" 
                SortExpression="marca" ReadOnly="true" />

            <asp:BoundField DataField="modelo" HeaderText="Modelo" 
                SortExpression="modelo" ReadOnly="true" />

 
            <asp:BoundField DataField="serial" HeaderText="Serial" 
                SortExpression="serial" ReadOnly="true" />
                

 
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
     
     
</div>    

</asp:Content>