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

public partial class Administracion_Dominio_ListadoModelo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarModelos();
        }
    }

    private void cargarModelos()
    {

        List<Modelo> modelos = Modelo.obtenerListadoModelos();
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("nombre");
        dataTable.Columns.Add("marca");

        foreach (Modelo p in modelos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = p.id;
            dataRow["nombre"] = p.nombre;
            dataRow["marca"] = p.marca;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdModelo.DataSource = dataTable;
        grdModelo.DataBind();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieModelo, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarModelo.aspx");

    }

    protected void grdModelo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editModelo")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieModelo, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarModelo.aspx");
        }
        if (e.CommandName == "eliminarModelo")
        {
            Dominio dominio = new Dominio();

            if (!dominio.verificarUsoDominio(e.CommandArgument.ToString(), GlobalEnum.dominios.Modelo.ToString()))
            {
                lblMensaje.Text = Resources.Mensajes.Dominio_Uso;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!dominio.eliminarDominio(e.CommandArgument.ToString(), GlobalEnum.dominios.Modelo.ToString()))
            {
                lblMensaje.Text = Resources.Mensajes.Error_Dominio_Eliminar;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Eliminar_Dominio;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                cargarModelos();
            }
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdModelo.PageIndex = e.NewPageIndex;
        cargarModelos();
    } 

}
