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

public partial class Desgaste_Default : System.Web.UI.Page
{
    Desgaste desgaste = new Desgaste();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDesgaste));

            }
            catch
            {
                this.id = 0;
            }

            cargarInventario();
        }
    }

    private void cargarInventario()
    {
        Existencia existencia = new Existencia();
        List<Existencia> existencias = existencia.obtenerListaExistencias(this.id);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("cantidadAnterior");
        dataTable.Columns.Add("cantidad");
        dataTable.Columns.Add("cantidadActual");
        dataTable.Columns.Add("fecha");
        dataTable.Columns.Add("obs");
        dataTable.Columns.Add("unidad");
        dataTable.Columns.Add("responsable");
        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ubicacion");
        dataTable.Columns.Add("usuario");

        foreach (Existencia a in existencias)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["cantidadAnterior"] = a.cantidadAnterior;
            dataRow["cantidad"] = a.cantidad;
            dataRow["cantidadActual"] = a.cantidadActual;
            dataRow["fecha"] = a.fecha;
            dataRow["obs"] = a.obs;
            dataRow["unidad"] = a.unidad;
            dataRow["usuario"] = a.usuario;
            dataRow["responsable"] = a.responsable;
            dataRow["empresa"] = a.empresa;
            dataRow["ubicacion"] = a.ubicacion;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdArticulo.DataSource = dataTable;
        grdArticulo.DataBind();
    }


 

    protected void grdArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo.PageIndex = e.NewPageIndex;
        cargarInventario();
    }
    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AgregarExistencia.aspx");
    }
}
