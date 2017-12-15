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

public partial class Administracion_Empresa_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            cargarEmpresa();

        }
    }
    private void cargarEmpresa()
    {

        List<Empresa> empresas = Empresa.obtenerListadoEmpresa(false);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("nombre");
        dataTable.Columns.Add("rif");
        dataTable.Columns.Add("telefono");
        dataTable.Columns.Add("direccion");
        dataTable.Columns.Add("descripcion");

        foreach (Empresa e in empresas)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = e.id;
            dataRow["nombre"] = e.nombre;
            dataRow["rif"] = e.rif;
            dataRow["telefono"] = e.telefono == "" ? "N/A" : e.telefono;
            dataRow["direccion"] = e.direccion == "" ? "N/A" : e.direccion;
            dataRow["descripcion"] = e.descripcion == "" ? "N/A" : e.descripcion;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdEmpresa.DataSource = dataTable;
        grdEmpresa.DataBind();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieEmpresa, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarEmpresa.aspx");
    }

    protected void grdEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editEmpresa")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieEmpresa, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarEmpresa.aspx");
        }
    }



    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmpresa.PageIndex = e.NewPageIndex;
        cargarEmpresa();
    }

}
