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

public partial class Matenimiento_Ubicacion_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarUbicacion();
        }
    }


    private void cargarUbicacion()
    {

        List<Ubicacion> ubicaciones = Ubicacion.obtenerListadoUbicacionEmpresa(0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("nombre");
        dataTable.Columns.Add("descripcion");
        dataTable.Columns.Add("empresa");

        foreach (Ubicacion u in ubicaciones)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = u.id;
            dataRow["nombre"] = u.nombre;
            dataRow["descripcion"] = u.descripcion;
            dataRow["empresa"] = u.empresa;

            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdUbicacion.DataSource = dataTable;
        grdUbicacion.DataBind();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieUbicacion, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarUbicacion.aspx");
    }

    protected void grdUbicacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editUbicacion")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieUbicacion, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarUbicacion.aspx");
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUbicacion.PageIndex = e.NewPageIndex;
        cargarUbicacion();
    }

}
