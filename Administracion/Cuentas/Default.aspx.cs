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

public partial class Administracion_Cuentas_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarCuentas();
        }
    }


    private void cargarCuentas()
    {

        List<Cuenta> cuentas = Cuenta.obtenerListadoCuentas(Request.Cookies[cookiesNombre.cookieApp]["id"].ToString());
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("usuario");
        dataTable.Columns.Add("responsable");
        dataTable.Columns.Add("rol");

        foreach (Cuenta u in cuentas)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = u.id;
            dataRow["usuario"] = u.usuario;
            dataRow["responsable"] = u.responsable;
            dataRow["rol"] = u.rol;

            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdCuentas.DataSource = dataTable;
        grdCuentas.DataBind();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCuenta, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarCuenta.aspx");
    }

    protected void grdCuentas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editCuenta")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieCuenta, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarCuenta.aspx");
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCuentas.PageIndex = e.NewPageIndex;
        cargarCuentas();
    } 

    

}
