<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     
 <div align="center">
       <h2><asp:Label ID="lblMesanje" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label> </h2>
                
        <div id="login" >
        
        <h2>Inicio de Session</h2>

        <table border="0">
          <tr>
             <td><span>Usuario</span></td>
             <td><asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
             <td><span>Clave</span></td>
             <td><asp:TextBox ID="txtClave" runat="server" TextMode="Password" ></asp:TextBox></td>
          </tr>
           </table>
       
          <div><asp:CheckBox class="texto-blanco" ID="chkRecorcar" runat="server" Text="No cerrar session" /></div>
          <br />
          <div>
          <asp:ImageButton ID="Button1" runat="server"  onclick="Button1_Click" ImageUrl="~/Imagenes/login.png" />
          </div>
          <br />
        </div>
</div> 
</asp:Content>


