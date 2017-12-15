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

public partial class Administracion_Categoria_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarCategoria();
        }
    }

    private void cargarCategoria()
    {

        List<Categoria> categorias = Categoria.obtenerCategorias();
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("nombre");
        dataTable.Columns.Add("prefijo");
        dataTable.Columns.Add("ejemplo1");
        dataTable.Columns.Add("ejemplo2");

        foreach (Categoria p in categorias)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = p.id;
            dataRow["nombre"] = p.nombre;
            dataRow["prefijo"] = p.prefijo;
            dataRow["ejemplo1"] = p.ejemplo1;
            dataRow["ejemplo2"] = p.ejemplo2;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdCategoria.DataSource = dataTable;
        grdCategoria.DataBind();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCategoria, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarCategoria.aspx");
    }

    protected void grdCategoria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editCategoria")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieCategoria, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarCategoria.aspx");
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCategoria.PageIndex = e.NewPageIndex;
        cargarCategoria();
    } 


}
