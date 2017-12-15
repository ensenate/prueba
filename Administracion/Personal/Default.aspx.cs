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
using System.Collections.Generic;

public partial class Matenimiento_Personal_Default : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            cargarResponsables(0);
            cargarResponsables(1);
        }

    }

    private void cargarResponsables(int tipo)
    {
  
        List<Responsable> personas = Responsable.obtenerListaPersonal(tipo);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id",typeof(int));
        dataTable.Columns.Add("nombre");
        dataTable.Columns.Add("rif");
        dataTable.Columns.Add("telefono");
        dataTable.Columns.Add("correo");
        dataTable.Columns.Add("tipo");
        dataTable.Columns.Add("empresa");

        foreach (Responsable p in personas)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = p.id;
            dataRow["rif"] = p.rif;
            dataRow["nombre"] = p.nombre;
            dataRow["telefono"] = p.telefono == "" ? "N/A" : p.telefono;
            dataRow["correo"] = p.correo == "" ? "N/A" : p.correo;
            dataRow["tipo"] = p.tipo==true?"Si":"No";
            dataRow["empresa"] = p.empresa;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        if (tipo == 1)
        {
            grdPersonal.DataSource = dataTable;
            grdPersonal.DataBind();
        }
        else {
            grdPersonal2.DataSource = dataTable;
            grdPersonal2.DataBind();
        }
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookiePersonal, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarPersonal.aspx");
    }

    protected void grdPersonal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editPersonal")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookiePersonal, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarPersonal.aspx");
        }
    }


    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPersonal.PageIndex = e.NewPageIndex;
        cargarResponsables(1);
    }


    protected void grdPersonal_RowCommand2(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editPersonal")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookiePersonal, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarPersonal.aspx");
        }
    }


    protected void grd_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {
        grdPersonal.PageIndex = e.NewPageIndex;
        cargarResponsables(0);
    }
}
