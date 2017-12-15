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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;

public partial class Reportes_GenerarEtiqueta : System.Web.UI.Page
{

    public int id
    {
        get
        {
            if (ViewState["id"] == null) return 0;
            return Int32.Parse(ViewState["id"].ToString());

        }
        set
        {
            ViewState["id"] = value;

        }
    }

    string sitioTemporal = ConfigurationManager.AppSettings["pathEtiqueta"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        generarEtiqueta();
    }



    public void generarEtiqueta()
    {
        try
        {
            this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieImprimir));

        }
        catch
        {
            this.id = 0;
        }


        Articulo articulo = new Articulo();
        articulo.obtenerArticuloId(this.id);

        string empresa = articulo.empresa;
        string P_OutputStream = articulo.control + ".pdf";

        string newFile = Server.MapPath(P_OutputStream);


        Document pdfDoc = new Document(PageSize.A4, 0, 0, 10, 0);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath(P_OutputStream), FileMode.OpenOrCreate));

        pdfDoc.Open();

        Paragraph parrafo = new Paragraph();

        parrafo.Alignment = Element.ALIGN_LEFT;
        parrafo.Font = FontFactory.GetFont("Arial", 15);
        parrafo.Font.SetStyle(Font.BOLD);
        parrafo.Font.SetStyle(Font.UNDEFINED);
        parrafo.Add(articulo.control);

        Paragraph parrafo2 = new Paragraph();
        parrafo2.Alignment = Element.ALIGN_LEFT;
        parrafo2.Font = FontFactory.GetFont("Arial", 7);
        parrafo2.Font.SetStyle(Font.BOLD);
        string fecha = DateTime.Now.ToShortDateString();
        parrafo2.Add(empresa + "  -  " + fecha);


        pdfDoc.Add(parrafo2);
        pdfDoc.Add(parrafo);


        pdfDoc.Close();
        ReadPdfFile(P_OutputStream);

    }


    private void ReadPdfFile(string P_OutputStream)
    {
        string path = sitioTemporal + P_OutputStream;
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(path);

        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.WriteFile(path);
            Response.End();
            //Response.AddHeader("content-length", buffer.Length.ToString());
            //Response.BinaryWrite(buffer);
        }

    }

}
