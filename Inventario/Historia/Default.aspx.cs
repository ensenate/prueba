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

public partial class Inventario_Historia_Default : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieArticulo));

            }
            catch
            {
                this.id = 0;
            }

            if (this.id == 0)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Articulo articulo = new Articulo();

                if (articulo.obtenerArticuloId(id))
                {
                    if (articulo.articulo_id != 0)
                    {
                        btnAgregar.Visible = false;
                    }
                }
            }

            cargarHistoria();
        }
    }

    private void cargarHistoria()
    {
        Articulo articulo = new Articulo();

        if (articulo.obtenerArticuloId(this.id))
        {

            ltrCategoria.Text = articulo.categoria;
            ltrMarca.Text = articulo.marca;
            ltrModelo.Text = articulo.modelo;
            ltrActivo.Text = articulo.activo == true ? "Si" : "No";
            ltrControl.Text = articulo.control;

            Historial historia = new Historial();
            List<Historial> historias = historia.obtenerListaHistoriasReferencias(this.id);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Inicio");
            dataTable.Columns.Add("Fin");
            dataTable.Columns.Add("Observacion");
            dataTable.Columns.Add("Empresa");
            dataTable.Columns.Add("Ubicacion");
            dataTable.Columns.Add("Responsable");
            dataTable.Columns.Add("Realizado");
            dataTable.Columns.Add("Autorizado");
            dataTable.Columns.Add("Estado");



            foreach (Historial h in historias)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["Inicio"]=h.fecha_inicio;
                dataRow["Fin"] = h.fecha_fin;
                dataRow["Observacion"] = h.obs;
                dataRow["Empresa"] = h.empresa;
                dataRow["Ubicacion"] = h.ubicacion;
                dataRow["Responsable"] = h.responsable;
                dataRow["Realizado"] = h.realizado;
                dataRow["Autorizado"] = h.autorizado;
                dataRow["Estado"] = h.estado;
                dataTable.Rows.Add(dataRow);

            }

            dataTable.AcceptChanges();
            grdHistorial.DataSource = dataTable;
            grdHistorial.DataBind();
        }
        else {
            Response.Redirect("../Default.aspx");
        }
    }



    protected void grdHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHistorial.PageIndex = e.NewPageIndex;
        cargarHistoria();
    }

    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieArticulo, this.id.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarHistorial.aspx");
    }
}
