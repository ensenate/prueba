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

public partial class CustomControls_BuscarArticulo : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        cargarInventario();
    }
    private void cargarInventario()
    {
        Articulo articulo = new Articulo();

        List<Articulo> articulos = articulo.obtenerListaArticulosReferencias("where tabla.borrado='N'",String.Empty,0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");

        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ubicacion");


        foreach (Articulo a in articulos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["serial"] = a.serial;
            dataRow["control"] = a.control;
            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["categoria"] = a.categoria;

            dataRow["empresa"] = a.empresa;
            dataRow["ubicacion"] = a.ubicacion;


            dataTable.Rows.Add(dataRow);

        }


        dataTable.AcceptChanges();
        grdArticulo.DataSource = dataTable;
        grdArticulo.DataBind();



    }

    protected void grdArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        txtBuscar.Text = string.Empty;

        if (e.CommandName == "seleccionar")
        {

             String valor = e.CommandArgument.ToString();
             txtBuscar.Text = string.Empty;
             ScriptManager.RegisterStartupScript(this, this.GetType(), "traerControl", "traerValor('" + valor + "');", true);
            
            this.Dispose();
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo.PageIndex = e.NewPageIndex;
        cargarInventario();
    }

    protected void grdArticulo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdArticulo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
