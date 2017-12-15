using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using System.Runtime.Serialization;
using Microsoft.Reporting.WebForms;
using CrystalDecisions.Shared;
using System.IO;

public partial class Reportes_VerReporte : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string n = string.Empty;

        try
        {
            n = Request["r"].ToString();
        }
        catch { }
        if (n != string.Empty)
        {
            generar(n);
        }
        else {
            Response.Redirect("../Invetario/Default.aspx");
        }

    }

    public void generar(String nReporte) {
        try
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=Report.pdf");
            HttpContext.Current.Response.ContentType = "application/pdf";
            MemoryStream objStream;

            DataGeneral data = new DataGeneral();
            Reportes reporte = new Reportes();
            ReportDocument documento = new ReportDocument();
            String ruta = "";

            crvReporte.ToolbarImagesFolderUrl = "../Imagenes/CrystalReport/toolbar/";
  
            switch (nReporte)
            {
                case "1":

                    reporte.listaActivosAsociaciones(data.PadreHijos);
                    ruta = Server.MapPath("ArticulosAsociaciones.rpt");
                    documento.Load(ruta);
                    documento.SetDataSource(data.Tables["PadreHijos"]);
                    crvReporte.ReportSource = documento;

                    break;

                case "2":
                        reporte.listadoEtiquetas(data.Etiqueta, string.Empty,string.Empty);
                  //      reporte.listadoEtiquetas(data.Etiqueta, Session["filtro"].ToString(), Session["keys"].ToString());
                        ruta = Server.MapPath("REtiqueta.rpt");
                        documento.Load(ruta);
                        documento.SetDataSource(data.Tables["Etiqueta"]);
                        crvReporte.ReportSource = documento;
                    
                        break;

                case "3":

                        reporte.listadoRetirados(data.Retirados);
                        ruta = Server.MapPath("Retirados.rpt");
                        documento.Load(ruta);
                        documento.SetDataSource(data.Tables["Retirados"]);
                        crvReporte.ReportSource = documento;

                        break;

                default:
                    Response.Redirect("../Inventario/Default.aspx");
                    break;
            }


            objStream = (MemoryStream)documento.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(objStream.ToArray());

        }
        catch (Exception ex) { Response.Redirect("../Inventario/Default.aspx"); }

    }

    public bool GetImpresoraDefecto()
    {
        for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        {
            PrinterSettings a = new PrinterSettings();
            a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
            if (PrinterSettings.InstalledPrinters[i].ToString() == "ETIQUETA")
            {
                return true;

            }
        }
        return false;
    }


    private void prueba(){



    }


    protected void crvReporte_Init(object sender, EventArgs e)
    {

    }
}
