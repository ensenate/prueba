<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reportes_Default" Title="Reportes del Sistema" %>



<%@ Register src="../CustomControls/MenuReporte.ascx" tagname="MenuReporte" tagprefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MenuReporte ID="MenuReporte1" runat="server" />
</asp:Content>

