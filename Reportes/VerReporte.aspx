
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerReporte.aspx.cs" Inherits="Reportes_VerReporte"   Title="Reportes del Sistema" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <CR:CrystalReportViewer ID="crvReporte" runat="server" AutoDataBind="True" 
            Height="50px" Width="350px" DisplayGroupTree="False" 
            EnableDatabaseLogonPrompt="False" EnableDrillDown="False" 
            EnableParameterPrompt="False" EnableTheming="False" EnableToolTips="False" 
            HasCrystalLogo="False" HasDrillUpButton="True" HasGotoPageButton="True" 
            HasSearchButton="True" HasToggleGroupTreeButton="False" 
            HasViewList="False" oninit="crvReporte_Init" DisplayPage="true" 
            HasExportButton="True" HasPageNavigationButtons="True" HasPrintButton="True" 
            HasZoomFactorList="False" SeparatePages="True"   
             />
    

    
    </div>
    </form>
</body>
</html>
